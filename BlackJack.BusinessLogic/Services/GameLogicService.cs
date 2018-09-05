using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;

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
            await _logRepository.CreateLogRoundIsStarted(gameId);

            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            List<Card> deck = await CreateDeck();

            await FirstCardsDistribution(gamePlayers, deck);
            await FirstCardCheck(gamePlayers);

            Game game = await _gameRepository.Get(gameId);
            game.Stage = StageHelper.FirstCardsDistribution;
            await _gameRepository.Update(game);
            await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);
        }

        public async Task<bool> IsHumanBjAndDealerBjDanger(int gameId)
        {
            bool humanBjAndDealerBjDanger = false;

            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(m => m.Player.IsHuman).First();
            if (human.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                humanBjAndDealerBjDanger = true;
            }

            return humanBjAndDealerBjDanger;
        }

        public async Task HumanBjAndDealerBjDangerContinueRound(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(m => m.Player.IsHuman).FirstOrDefault();
            human.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;
            await _gamePlayerRepository.Update(human);
        }

        public async Task AddOneMoreCardToHuman(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();
            List<Card> deck = await ResumeDeck(gamePlayers);

            await AddingCardToPlayer(human, deck);
        }

        public async Task<bool> CanHumanTakeOneMoreCard(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();

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
            await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);
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
            GamePlayer dealer = gamePlayers.Where(m => m.Player.IsDealer).First();
            List<PlayerCard> dealerPlayerCards = (await _playerCardRepository.GetByGamePlayerId(dealer.Id)).ToList();
            Card dealerFirstCard = dealerPlayerCards[0].Card;
            bool dealerBjDanger = _cardCheckProvider.DealerBjDanger(dealerFirstCard.CardName);

            if (dealerBjDanger)
            {
                await _logRepository.CreateLogDealerBjDanger(gamePlayers.First().GameId, dealer.Player, dealerFirstCard.Id, dealerFirstCard.CardName, dealerFirstCard.ToString());
            }

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (!gamePlayer.Player.IsDealer)
                {
                    int playerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(gamePlayer.Id);
                    gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundFirstPhaseResult(gamePlayer.RoundScore, playerCardsCount, dealerBjDanger);
                    await _gamePlayerRepository.Update(gamePlayer);

                    if (gamePlayer.BetPayCoefficient == BetValueHelper.BetBjCoefficient)
                    {
                        await _logRepository.CreateLogPlayerBj(gamePlayer.Id, gamePlayer.Player, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                    }

                    if (gamePlayer.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
                    {
                        await _logRepository.CreateLogPlayerBjAndDealerBjDanger(gamePlayer.Id, gamePlayer.Player, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                    }
                }
            }
        }

        private async Task SecondCardsDistribution(IEnumerable<GamePlayer> players, List<Card> deck)
        {
            foreach (GamePlayer gamePlayer in players)
            {
                if (!gamePlayer.Player.IsHuman)
                {
                    await SecondCardAddingToBot(gamePlayer, deck);
                }
            }
        }

        private async Task SecondCardCheck(IEnumerable<GamePlayer> gamePlayers)
        {
            GamePlayer dealer = gamePlayers.Where(m => m.Player.IsDealer).FirstOrDefault();
            int dealerPlayerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(dealer.Id);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (!gamePlayer.Player.IsDealer)
                {
                    int playerCardsCount = await _playerCardRepository.GetCountByGamePlayerId(gamePlayer.Id);
                    gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundSecondPhaseResult(gamePlayer.Bet, gamePlayer.RoundScore, playerCardsCount, dealer.RoundScore, dealerPlayerCardsCount, gamePlayer.BetPayCoefficient);
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
            await _logRepository.CreateLogCardIsAdded(gamePlayer.GameId, gamePlayer.Player, gamePlayer.RoundScore, card.Id, card.CardName, _playerCardProvider.ConvertCardToString(card));
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

        private int CardToIntConverter(Card card)
        {
            int id;
            id = card.Id;
            return id;
        }

        private List<Card> ShuffleDeck(List<Card> cards)
        {
            List<Card> shuffledCards = cards;

            Random random = new Random();
            int card1;
            int card2;
            Card glass;

            for (int i = 0; i < shuffledCards.Count; i++)
            {
                card1 = random.Next(shuffledCards.Count);
                card2 = random.Next(shuffledCards.Count);

                glass = shuffledCards[card1];
                shuffledCards[card1] = shuffledCards[card2];
                shuffledCards[card2] = glass;
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