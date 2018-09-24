using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels;
using AutoMapper;

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


        public GameLogicService(IPlayerRepository playerRepository, IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, 
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

        public async Task<string> ValidateBet(int bet, int gamePlayerId)
        {
            string validationMessage = string.Empty;
            int score = await _gamePlayerRepository.GetScoreById(gamePlayerId);

            if (bet > score)
            {
                validationMessage = GameMessageHelper.BetMoreThanScore;
            }

            if (bet <= BetValueHelper.BetZero)
            {
                validationMessage = GameMessageHelper.BetLessThanMin;
            }

            return validationMessage;
        }

        public async Task<GameLogicRoundFirstPhaseResponseView> DoRoundFirstPhase(int bet, int gameId)
        {
            var logs = new List<Log>();
            logs.Add(new Log() { GameId = gameId, DateTime = DateTime.Now, Message = LogMessageHelper.NewRoundStarted()});

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
            logs.Add(new Log() { GameId = gameId, DateTime = DateTime.Now, Message = LogMessageHelper.GameStageChanged(StageHelper.FirstCardsDistribution) });
            await _logRepository.CreateMany(logs);

            GameLogicRoundFirstPhaseResponseView gameLogicResponseView = GetGameLogicRoundFirstPhaseResponseView(bots, dealer, human);
            gameLogicResponseView.Id = gameId;
            return gameLogicResponseView;
        }
        
        public async Task<GameLogicRoundFirstPhaseResponseView> ResumeGameAfterRoundFirstPhase(int gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);

            GameLogicRoundFirstPhaseResponseView gameLogicResponseView = GetGameLogicRoundFirstPhaseResponseView(bots, dealer, human);
            gameLogicResponseView.Id = gameId;
            return gameLogicResponseView;
        }
        
        public async Task<GameLogicAddCardToHumanView> AddCardToHuman(int gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            IEnumerable<int> cardOnHandsIds = await _playerCardRepository.GetCardsOnHandsIdsByGameId(gameId);
            List<Card> deck = await ResumeDeck(cardOnHandsIds);
            var logs = new List<Log>();

            PlayerCard addedPlayerCard = AddCardToPlayer(human, deck, logs);
            human.PlayerCards.Add(addedPlayerCard);
            human.RoundScore = CountCardScore(human.PlayerCards);
            human.CardAmount = ++human.CardAmount;
            await _playerCardRepository.Create(addedPlayerCard);
            await _gamePlayerRepository.UpdateAfterAddingOneMoreCard(human);
            await _logRepository.CreateMany(logs);

            GameLogicAddCardToHumanView addOneMoreCardToHumanViewModel = Mapper.Map<GamePlayer, GameLogicAddCardToHumanView>(human);
            addOneMoreCardToHumanViewModel.CanHumanTakeOneMoreCard = !_gamePlayerProvider.DoesHumanHaveEnoughCards(human.RoundScore);
            return addOneMoreCardToHumanViewModel;
        }

        public async Task<GameLogicRoundSecondPhaseResponseView> DoRoundSecondPhase(int gameId, bool blackJackDangerContinueRound = false)
        {
            var logs = new List<Log>();
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);
            
            if (blackJackDangerContinueRound)
            {
                human.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;
            }

            IEnumerable<int> cardOnHandsIds = await _playerCardRepository.GetCardsOnHandsIdsByGameId(gameId);
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
            logs.Add(new Log() { GameId = gameId, DateTime = DateTime.Now, Message = LogMessageHelper.GameStageChanged(StageHelper.SecondCardsDistribution) });
            await _logRepository.CreateMany(logs);

            GameLogicRoundSecondPhaseResponseView gameLogicResponseView = GetGameLogicRoundSecondPhaseResponseView(bots, dealer, human);
            gameLogicResponseView.Id = gameId;
            return gameLogicResponseView;
        }

        public async Task<GameLogicRoundSecondPhaseResponseView> ResumeGameAfterRoundSecondPhase(int gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Human);
            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerWithCards(gameId, (int)PlayerType.Dealer);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersWithCards(gameId, (int)PlayerType.Bot);

            GameLogicRoundSecondPhaseResponseView gameLogicResponseView = GetGameLogicRoundSecondPhaseResponseView(bots, dealer, human);
            gameLogicResponseView.Id = gameId;
            return gameLogicResponseView;
        }

        public async Task EndRound(int gameId)
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
                gamePlayer.RoundScore = GameValueHelper.ZeroScore;
                gamePlayer.CardAmount = GameValueHelper.ZeroScore;
            }

            await _gamePlayerRepository.UpdateMany(gamePlayers);
            await _gamePlayerRepository.DeleteBotsWithZeroScore(gameId);
        }
        
        public async Task EndGame(GameLogicEndGameView GameLogicEndGameView)
        {
            await _gameRepository.UpdateResult(GameLogicEndGameView.GameId, GameLogicEndGameView.GameResult);
            await _gamePlayerRepository.DeleteAllByGameId(GameLogicEndGameView.GameId);
        }

        private GameLogicRoundFirstPhaseResponseView GetGameLogicRoundFirstPhaseResponseView(IEnumerable<GamePlayer> bots, GamePlayer dealer, GamePlayer human)
        {
            var gameLogicResponseView = new GameLogicRoundFirstPhaseResponseView();
            gameLogicResponseView.Dealer = Mapper.Map<GamePlayer, GamePlayerGameLogicResponseItem>(dealer);
            gameLogicResponseView.Dealer.RoundScore = GameValueHelper.ZeroScore;
            gameLogicResponseView.Dealer.Cards.Clear();
            gameLogicResponseView.Dealer.Cards.Add(CardToStringHelper.Convert(dealer.PlayerCards[0].Card));
            gameLogicResponseView.Human = Mapper.Map<GamePlayer, GamePlayerGameLogicResponseItem>(human);
            gameLogicResponseView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerGameLogicResponseItem>>(bots);
            gameLogicResponseView.CanHumanTakeOneMoreCard = !_gamePlayerProvider.DoesHumanHaveEnoughCards(human.RoundScore);
            gameLogicResponseView.HumanBlackJackAndDealerBlackJackDanger = IsHumanBlackJackAndDealerBlackJackDanger(human);
            return gameLogicResponseView;
        }

        private GameLogicRoundSecondPhaseResponseView GetGameLogicRoundSecondPhaseResponseView(IEnumerable<GamePlayer> bots, GamePlayer dealer, GamePlayer human)
        {
            var gameLogicResponseView = new GameLogicRoundSecondPhaseResponseView();
            gameLogicResponseView.Dealer = Mapper.Map<GamePlayer, GamePlayerGameLogicResponseItem>(dealer);
            gameLogicResponseView.Human = Mapper.Map<GamePlayer, GamePlayerGameLogicResponseItem>(human);
            gameLogicResponseView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerGameLogicResponseItem>>(bots);
            gameLogicResponseView.RoundResult = _gamePlayerProvider.GetHumanRoundResult(human.BetPayCoefficient);
            return gameLogicResponseView;
        }

        private bool IsHumanBlackJackAndDealerBlackJackDanger(GamePlayer human)
        {
            bool humanBlackJackAndDealerBlackJackDanger = false;

            if (human.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                humanBlackJackAndDealerBlackJackDanger = true;
            }

            return humanBlackJackAndDealerBlackJackDanger;
        }

        private async Task DistributeRoundFirstPhaseCards(IEnumerable<GamePlayer> gamePlayers, List<Card> deck, List<Log> logs)
        {
            var playerCardsInsert = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.PlayerCards = new List<PlayerCard>();

                PlayerCard firstCard = AddCardToPlayer(gamePlayer, deck, logs);
                gamePlayer.PlayerCards.Add(firstCard);
                PlayerCard secondCard = AddCardToPlayer(gamePlayer, deck, logs);
                gamePlayer.PlayerCards.Add(secondCard);

                gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
                gamePlayer.CardAmount = gamePlayer.PlayerCards.Count();
                playerCardsInsert.AddRange(gamePlayer.PlayerCards);
            }

            await _playerCardRepository.CreateMany(playerCardsInsert);
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
                DateTime = DateTime.Now,
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

        private async Task<List<Card>> ResumeDeck(IEnumerable<int> cardOnHandsIds)
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