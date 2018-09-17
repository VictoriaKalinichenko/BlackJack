using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Services
{
    public class GameLogicService : IGameLogicService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICardRepository _cardRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;
        

        public GameLogicService(IPlayerRepository playerRepository, IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            IGamePlayerProvider gamePlayerProvider, IPlayerCardProvider playerCardProvider, ICardCheckProvider cardCheckProvider, ILogRepository logRepository, ICardRepository cardRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _logRepository = logRepository;
            _cardRepository = cardRepository;

            _gamePlayerProvider = gamePlayerProvider;
            _playerCardProvider = playerCardProvider;
            _cardCheckProvider = cardCheckProvider;
        }

        public async Task RoundFirstPhase(int gameId)
        {
            string message = LogMessageHelper.NewRoundStarted();
            await _logRepository.Create(gameId, message);

            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            List<Card> deck = await CreateDeck();

            await FirstCardsDistribution(gamePlayers, deck);
            await FirstCardCheck(gamePlayers);

            Game game = await _gameRepository.Get(gameId);
            game.Stage = StageHelper.FirstCardsDistribution;
            await _gameRepository.Update(game);

            message = LogMessageHelper.GameStageChanged(game.Stage);
            await _logRepository.Create(game.Id, message);
        }

        public async Task<bool> IsHumanBlackJackAndDealerBlackJackDanger(int gameId)
        {
            bool humanBlackJackAndDealerBlackJackDanger = false;

            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerByGameId(gameId, (int)PlayerType.Human);
            if (human.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                humanBlackJackAndDealerBlackJackDanger = true;
            }

            return humanBlackJackAndDealerBlackJackDanger;
        }

        public async Task BlackJackDangerContinueRound(int gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerByGameId(gameId, (int)PlayerType.Human);
            human.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;
            await _gamePlayerRepository.Update(human);
        }

        public async Task AddOneMoreCardToHuman(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(m => m.Player.PlayerType == (int)PlayerType.Human).First();
            List<Card> deck = await ResumeDeck(gamePlayers);

            await AddingCardToPlayer(human, deck);
        }

        public async Task<bool> CanHumanTakeOneMoreCard(int gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerByGameId(gameId, (int)PlayerType.Human);

            bool canHumanTakeOneMoreCard = false;
            canHumanTakeOneMoreCard = !_cardCheckProvider.HumanPlayerHasEnoughCards(human.RoundScore);
            return canHumanTakeOneMoreCard;
        }

        public async Task RoundSecondPhase(int gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            List<Card> deck = await ResumeDeck(gamePlayers);

            await SecondCardsDistribution(gamePlayers, deck);
            await SecondCardCheck(gamePlayers);

            game.Stage = StageHelper.SecondCardsDistribution;
            await _gameRepository.Update(game);

            string message = LogMessageHelper.GameStageChanged(game.Stage);
            await _logRepository.Create(game.Id, message);
        }
        
        private async Task FirstCardsDistribution(IEnumerable<GamePlayer> players, List<Card> deck)
        {
            foreach (GamePlayer gamePlayer in players)
            {
                await AddingCardToPlayer(gamePlayer, deck);
                await AddingCardToPlayer(gamePlayer, deck);
            }
        }

        private async Task FirstCardCheck(IEnumerable<GamePlayer> gamePlayers)
        {
            GamePlayer dealer = gamePlayers.Where(m => (int)m.Player.PlayerType == (int)PlayerType.Dealer).First();
            List<PlayerCard> dealerPlayerCards = (await _playerCardRepository.GetByGamePlayerId(dealer.Id)).ToList();
            Card dealerFirstCard = dealerPlayerCards[0].Card;
            bool dealerBlackJackDanger = _cardCheckProvider.DealerBlackJackDanger(dealerFirstCard.CardName);

            if (dealerBlackJackDanger)
            {
                string message = LogMessageHelper.DealerBlackJackDanger(dealer.Id, dealer.Player.Name, dealerFirstCard.Id, dealerFirstCard.CardName, _playerCardProvider.ConvertCardToString(dealerFirstCard));
                await _logRepository.Create(gamePlayers.First().GameId, message);
            }

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer))
                {
                    int playerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(gamePlayer.Id);
                    gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundFirstPhaseResult(gamePlayer.RoundScore, playerCardsCount, dealerBlackJackDanger);
                    await _gamePlayerRepository.Update(gamePlayer);

                    if (gamePlayer.BetPayCoefficient == BetValueHelper.BetBlackJackCoefficient)
                    {
                        string message = LogMessageHelper.PlayerBlackJack(gamePlayer.Player.PlayerType.ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                        await _logRepository.Create(gamePlayer.GameId, message);
                    }

                    if (gamePlayer.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
                    {
                        string message = LogMessageHelper.PlayerBlackJackAndDealerBlackJackDanger(gamePlayer.Player.PlayerType.ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                        await _logRepository.Create(gamePlayer.GameId, message);
                    }
                }
            }
        }

        private async Task SecondCardsDistribution(IEnumerable<GamePlayer> players, List<Card> deck)
        {
            foreach (GamePlayer gamePlayer in players)
            {
                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Human))
                {
                    await SecondCardAddingToBot(gamePlayer, deck);
                }
            }
        }

        private async Task SecondCardCheck(IEnumerable<GamePlayer> gamePlayers)
        {
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerByGameId(gamePlayers.First().GameId, (int)PlayerType.Dealer);
            int dealerPlayerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(dealer.Id);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer))
                {
                    int playerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(gamePlayer.Id);
                    gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundSecondPhaseResult(gamePlayer.RoundScore, playerCardsCount, dealer.RoundScore, dealerPlayerCardsCount, gamePlayer.BetPayCoefficient);
                    await _gamePlayerRepository.Update(gamePlayer);
                }
            }
        }

        private async Task AddingCardToPlayer(GamePlayer gamePlayer, List<Card> deck)
        {
            Card card = await _playerCardProvider.AddingCardToPlayer(gamePlayer.Id, deck);
            IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
            gamePlayer.RoundScore = _playerCardProvider.CardScoreCount(playerCards);

            await _gamePlayerRepository.Update(gamePlayer);

            string message = LogMessageHelper.CardAdded(card.Id, card.CardName, _playerCardProvider.ConvertCardToString(card), gamePlayer.Player.PlayerType.ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore);
            await _logRepository.Create(gamePlayer.GameId, message);
        }

        private async Task SecondCardAddingToBot(GamePlayer gamePlayer, List<Card> deck)
        {
            for (; !_cardCheckProvider.BotHasEnoughCards(gamePlayer.RoundScore);)
            {
                await AddingCardToPlayer(gamePlayer, deck);
            }
        }

        private async Task<List<Card>> CreateDeck()
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            deck = ShuffleDeck(deck);
            return deck;
        }

        private async Task<List<Card>> ResumeDeck(IEnumerable<GamePlayer> gamePlayers)
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
                deck = RemoveCardsOnHands(playerCards, deck);
            }

            deck = ShuffleDeck(deck);
            return deck;
        }

        private List<Card> ShuffleDeck(List<Card> cards)
        {
            List<Card> shuffledCards = cards;

            Random random = new Random();
            int cardFirst;
            int cardSecond;
            Card glass;

            for (int i = 0; i < shuffledCards.Count; i++)
            {
                cardFirst = random.Next(shuffledCards.Count);
                cardSecond = random.Next(shuffledCards.Count);

                glass = shuffledCards[cardFirst];
                shuffledCards[cardFirst] = shuffledCards[cardSecond];
                shuffledCards[cardSecond] = glass;
            }

            return shuffledCards;
        }

        private List<Card> RemoveCardsOnHands(IEnumerable<PlayerCard> playerCards, List<Card> cards)
        {
            List<Card> returnCards = cards;

            foreach (PlayerCard playerCard in playerCards)
            {
                if (returnCards.Where(m => m.Id == playerCard.CardId).Count() > 0)
                {
                    returnCards.Remove(returnCards.Where(m => m.Id == playerCard.CardId).First());
                }
            }

            return returnCards;
        }
    }
}