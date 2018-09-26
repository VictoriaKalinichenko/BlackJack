using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IGamePlayerProvider _gamePlayerProvider;


        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, 
            IPlayerCardRepository playerCardRepository, IGamePlayerProvider gamePlayerProvider, ILogRepository logRepository, ICardRepository cardRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _logRepository = logRepository;
            _cardRepository = cardRepository;
            _gamePlayerProvider = gamePlayerProvider;
        }

        public async Task<string> ValidateBet(int bet, long gamePlayerId)
        {
            string validationMessage = string.Empty;
            int score = await _gamePlayerRepository.GetScoreById(gamePlayerId);

            if (bet > score)
            {
                validationMessage = GameMessageHelper.BetMoreThanScore;
            }

            if (bet <= GameValueHelper.Zero)
            {
                validationMessage = GameMessageHelper.BetLessThanMin;
            }

            return validationMessage;
        }

        public async Task<DoRoundFirstPhaseResponseViewModel> DoRoundFirstPhase(int bet, long gameId)
        {
            var logs = new List<Log>();
            logs.Add(new Log() { GameId = gameId, Message = LogMessageHelper.NewRoundStarted()});

            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithoutCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithoutCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithoutCards(gameId, (int)PlayerType.Bot);
            _gamePlayerProvider.CreateBets(bots, human, bet, logs);

            List<Card> deck = await CreateDeck();
            List<GamePlayer> gamePlayers = bots.ToList();
            gamePlayers.Add(human);
            gamePlayers.Add(dealer);
            await DistributeRoundFirstPhaseCards(gamePlayers, deck, logs);
            gamePlayers.Remove(dealer);
            _gamePlayerProvider.CheckCardsInFirstTime(gamePlayers, dealer, logs);
            gamePlayers.Add(dealer);
            await _gamePlayerRepository.UpdateMany(gamePlayers);
            
            await _gameRepository.UpdateStage(gameId, StageHelper.FirstCardsDistribution);
            logs.Add(new Log() { GameId = gameId, Message = LogMessageHelper.GameStageChanged(StageHelper.FirstCardsDistribution) });
            await _logRepository.CreateMany(logs);

            DoRoundFirstPhaseResponseViewModel doRoundFirstPhaseResponseViewModel = GetDoRoundFirstPhaseResponseViewModel(bots, dealer, human);
            doRoundFirstPhaseResponseViewModel.Id = gameId;
            return doRoundFirstPhaseResponseViewModel;
        }
        
        public async Task<DoRoundFirstPhaseResponseViewModel> ResumeAfterRoundFirstPhase(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);

            DoRoundFirstPhaseResponseViewModel doRoundFirstPhaseResponseViewModel = GetDoRoundFirstPhaseResponseViewModel(bots, dealer, human);
            doRoundFirstPhaseResponseViewModel.Id = gameId;
            return doRoundFirstPhaseResponseViewModel;
        }
        
        public async Task<AddCardViewModel> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            IEnumerable<long> cardOnHandsIds = await _playerCardRepository.GetCardsOnHandsIdsByGameId(gameId);
            List<Card> deck = await ResumeDeck(cardOnHandsIds);
            var logs = new List<Log>();

            PlayerCard addedPlayerCard = AddCardToPlayer(human, deck, logs);
            human.PlayerCards.Add(addedPlayerCard);
            human.RoundScore = CountCardScore(human.PlayerCards);
            human.CardAmount = ++human.CardAmount;
            await _playerCardRepository.Create(addedPlayerCard);
            await _gamePlayerRepository.UpdateAfterAddingOneMoreCard(human);
            await _logRepository.CreateMany(logs);

            AddCardViewModel addCardViewModel = Mapper.Map<GamePlayer, AddCardViewModel>(human);
            addCardViewModel.CanTakeCard = !_gamePlayerProvider.DoesHumanHaveEnoughCards(human.RoundScore);
            return addCardViewModel;
        }

        public async Task<DoRoundSecondPhaseResponseViewModel> DoRoundSecondPhase(long gameId, bool blackJackDangerContinueRound = false)
        {
            var logs = new List<Log>();
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);
            
            if (blackJackDangerContinueRound)
            {
                human.BetPayCoefficient = BetValueHelper.DefaultCoefficient;
            }

            IEnumerable<long> cardOnHandsIds = await _playerCardRepository.GetCardsOnHandsIdsByGameId(gameId);
            List<Card> deck = await ResumeDeck(cardOnHandsIds);

            List<GamePlayer> gamePlayers = bots.ToList();
            gamePlayers.Add(dealer);
            await DistributeRoundSecondPhaseCards(gamePlayers, deck, logs);
            gamePlayers.Remove(dealer);
            gamePlayers.Add(human);
            _gamePlayerProvider.CheckCardsInSecondTime(gamePlayers, dealer, logs);
            gamePlayers.Add(dealer);
            await _gamePlayerRepository.UpdateManyAfterRoundSecondPhase(gamePlayers);

            await _gameRepository.UpdateStage(gameId, StageHelper.SecondCardsDistribution);
            logs.Add(new Log() { GameId = gameId, Message = LogMessageHelper.GameStageChanged(StageHelper.SecondCardsDistribution) });
            await _logRepository.CreateMany(logs);

            DoRoundSecondPhaseResponseViewModel doRoundSecondPhaseResponseViewModel = GetDoRoundSecondPhaseResponseViewModel(bots, dealer, human);
            doRoundSecondPhaseResponseViewModel.Id = gameId;
            return doRoundSecondPhaseResponseViewModel;
        }

        public async Task<DoRoundSecondPhaseResponseViewModel> ResumeAfterRoundSecondPhase(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);

            DoRoundSecondPhaseResponseViewModel doRoundSecondPhaseResponseViewModel = GetDoRoundSecondPhaseResponseViewModel(bots, dealer, human);
            doRoundSecondPhaseResponseViewModel.Id = gameId;
            return doRoundSecondPhaseResponseViewModel;
        }

        public async Task EndRound(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithoutCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithoutCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithoutCards(gameId, (int)PlayerType.Bot);
            
            List<GamePlayer> gamePlayers = bots.ToList();
            gamePlayers.Add(human);
            _gamePlayerProvider.PayRoundBets(gamePlayers, dealer);
            IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetPlayerCardsByGameId(gameId);
            await _playerCardRepository.DeleteMany(playerCards);
            await _gameRepository.UpdateStage(gameId, StageHelper.RoundStart);

            gamePlayers.Add(dealer);
            foreach(var gamePlayer in gamePlayers)
            {
                gamePlayer.RoundScore = GameValueHelper.Zero;
                gamePlayer.CardAmount = GameValueHelper.Zero;
            }

            await _gamePlayerRepository.UpdateMany(gamePlayers);
            await _gamePlayerRepository.DeleteBotsWithZeroScore(gameId);
        }
        
        public async Task EndGame(EndGameViewModel GameLogicEndGameView)
        {
            await _gameRepository.UpdateResult(GameLogicEndGameView.GameId, GameLogicEndGameView.Result);
            await _gamePlayerRepository.DeleteAllByGameId(GameLogicEndGameView.GameId);
        }

        private DoRoundFirstPhaseResponseViewModel GetDoRoundFirstPhaseResponseViewModel(IEnumerable<GamePlayer> bots, GamePlayer dealer, GamePlayer human)
        {
            var doRoundFirstPhaseResponseViewModel = new DoRoundFirstPhaseResponseViewModel();
            doRoundFirstPhaseResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerViewItem>(dealer);
            doRoundFirstPhaseResponseViewModel.Dealer.RoundScore = GameValueHelper.Zero;
            doRoundFirstPhaseResponseViewModel.Dealer.Cards.Clear();
            doRoundFirstPhaseResponseViewModel.Dealer.Cards.Add(CardToStringHelper.Convert(dealer.PlayerCards[0].Card));
            doRoundFirstPhaseResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerViewItem>(human);
            doRoundFirstPhaseResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerViewItem>>(bots);
            doRoundFirstPhaseResponseViewModel.CanTakeCard = !_gamePlayerProvider.DoesHumanHaveEnoughCards(human.RoundScore);
            doRoundFirstPhaseResponseViewModel.DealerBlackJackDanger = IsDealerBlackJackDanger(human);
            return doRoundFirstPhaseResponseViewModel;
        }

        private DoRoundSecondPhaseResponseViewModel GetDoRoundSecondPhaseResponseViewModel(IEnumerable<GamePlayer> bots, GamePlayer dealer, GamePlayer human)
        {
            var doRoundSecondPhaseResponseViewModel = new DoRoundSecondPhaseResponseViewModel();
            doRoundSecondPhaseResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerViewItem>(dealer);
            doRoundSecondPhaseResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerViewItem>(human);
            doRoundSecondPhaseResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerViewItem>>(bots);
            doRoundSecondPhaseResponseViewModel.RoundResult = _gamePlayerProvider.GetRoundResult(human.BetPayCoefficient);
            return doRoundSecondPhaseResponseViewModel;
        }

        private bool IsDealerBlackJackDanger(GamePlayer human)
        {
            bool dealerBlackJackDanger = false;

            if (human.BetPayCoefficient == BetValueHelper.WinCoefficient)
            {
                dealerBlackJackDanger = true;
            }

            return dealerBlackJackDanger;
        }

        private async Task DistributeRoundFirstPhaseCards(IEnumerable<GamePlayer> gamePlayers, List<Card> deck, List<Log> logs)
        {
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.PlayerCards = new List<PlayerCard>();

                PlayerCard firstCard = AddCardToPlayer(gamePlayer, deck, logs);
                gamePlayer.PlayerCards.Add(firstCard);
                PlayerCard secondCard = AddCardToPlayer(gamePlayer, deck, logs);
                gamePlayer.PlayerCards.Add(secondCard);

                gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
                gamePlayer.CardAmount = gamePlayer.PlayerCards.Count();
                playerCards.AddRange(gamePlayer.PlayerCards);
            }

            await _playerCardRepository.CreateMany(playerCards);
        }
        
        private async Task DistributeRoundSecondPhaseCards(IEnumerable<GamePlayer> players, List<Card> deck, List<Log> logs)
        {
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in players)
            {
                AddRoundSecondPhaseCardsToBot(gamePlayer, deck, logs, playerCards);
            }

            await _playerCardRepository.CreateMany(playerCards);
        }

        private void AddRoundSecondPhaseCardsToBot(GamePlayer gamePlayer, List<Card> deck, List<Log> logs, List<PlayerCard> playerCards)
        {
            for (; !_gamePlayerProvider.DoesBotHaveEnoughCards(gamePlayer.RoundScore);)
            {
                PlayerCard addedPlayerCard = AddCardToPlayer(gamePlayer, deck, logs);
                playerCards.Add(addedPlayerCard);
                gamePlayer.PlayerCards.Add(addedPlayerCard);
                gamePlayer.CardAmount = ++gamePlayer.CardAmount;
                gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
            }
        }
        
        private PlayerCard AddCardToPlayer(GamePlayer gamePlayer, List<Card> deck, List<Log> logs)
        {
            Card card = deck.First();
            deck.Remove(card);
            var playerCard = new PlayerCard() { GamePlayerId = gamePlayer.Id, CardId = card.Id, Card = card };
            var logFirstCard = new Log() { 
                GameId = gamePlayer.GameId,
                Message = LogMessageHelper.CardAdded(card.Id, card.Name, CardToStringHelper.Convert(card), ((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name)
            };
            logs.Add(logFirstCard);
            return playerCard;
        }

        private async Task<List<Card>> CreateDeck()
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            deck = ShuffleDeck(deck);
            return deck;
        }

        private async Task<List<Card>> ResumeDeck(IEnumerable<long> cardOnHandsIds)
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            foreach (var cardId in cardOnHandsIds)
            {
                deck.Remove(deck.Where(m => m.Id == cardId).First());
            }

            deck = ShuffleDeck(deck);
            return deck;
        }

        private List<Card> ShuffleDeck(List<Card> cards)
        {
            List<Card> shuffledCards = cards;

            var random = new Random();
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
        
        private int CountCardScore(IEnumerable<PlayerCard> playerCards)
        {
            int roundScore = 0;

            foreach (PlayerCard playerCard in playerCards)
            {
                int cardScore = playerCard.Card.Name;
                if (cardScore > (int)CardName.Ace)
                {
                    cardScore = (int)CardName.Ten;
                }
                roundScore += cardScore;
            }

            int aceCount = playerCards
                .Where(m => m.Card.Name == (int)CardName.Ace)
                .Count();

            for (; aceCount > 0 && roundScore > CardValueHelper.CardBlackJackScore;)
            {
                aceCount--;
                roundScore -= (int)CardName.Ten;
            }

            return roundScore;
        }
    }
}