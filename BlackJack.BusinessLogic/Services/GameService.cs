using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
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

        public async Task<StartRoundResponseViewModel> StartRound(int bet, long gameId)
        {
            var logs = new List<Log>();
            logs.Add(new Log() { GameId = gameId, Message = LogHelper.NewRoundStarted()});

            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            players.Remove(human);
            players.Remove(dealer);
            await _gamePlayerProvider.CreateBets(players, human, bet, logs);

            List<Card> deck = await CreateDeck();
            players.Add(human);
            players.Add(dealer);
            await DistributeFirstCards(players, deck);

            players.Remove(dealer);
            _gamePlayerProvider.DefinePayCoefficientsAfterRoundStart(players, dealer, logs);
            players.Add(dealer);
            await _gamePlayerRepository.UpdateMany(players);
            
            await _gameRepository.UpdateStage(gameId, StageHelper.FirstCardsDistribution);
            logs.Add(new Log() { GameId = gameId, Message = LogHelper.GameStageChanged(StageHelper.FirstCardsDistribution) });
            await _logRepository.CreateMany(logs);

            players.Remove(human);
            players.Remove(dealer);

            bool canTakeCard = !_gamePlayerProvider.IsEnoughCardsForHuman(human.RoundScore);
            bool blackJackChoice = IsBlackJackChoice(human);
            StartRoundResponseViewModel startRoundResponseViewModel = CustomMapper.GetStartRoundResponseViewModel(players, gameId, canTakeCard, blackJackChoice);
            return startRoundResponseViewModel;
        }
        
        public async Task<StartRoundResponseViewModel> ResumeAfterStartRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();            
            bool canTakeCard = !_gamePlayerProvider.IsEnoughCardsForHuman(human.RoundScore);
            bool blackJackChoice = IsBlackJackChoice(human);
            StartRoundResponseViewModel startRoundResponseViewModel = CustomMapper.GetStartRoundResponseViewModel(players, gameId, canTakeCard, blackJackChoice);
            return startRoundResponseViewModel;
        }
        
        public async Task<AddCardViewModel> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetWithCards(gameId, (int)PlayerType.Human);
            IEnumerable<Card> cardsOnHands = await _playerCardRepository.GetCardsOnHands(gameId);
            List<Card> deck = await ResumeDeck(cardsOnHands);
            var logs = new List<Log>();

            PlayerCard addedPlayerCard = AddCardToPlayer(human, deck, logs);
            human.PlayerCards.Add(addedPlayerCard);
            human.RoundScore = CountCardScore(human.PlayerCards);
            human.CardAmount++;
            await _playerCardRepository.Create(addedPlayerCard);
            await _gamePlayerRepository.UpdateAddingCard(human);
            await _logRepository.CreateMany(logs);

            AddCardViewModel addCardViewModel = Mapper.Map<GamePlayer, AddCardViewModel>(human);
            addCardViewModel.CanTakeCard = !_gamePlayerProvider.IsEnoughCardsForHuman(human.RoundScore);
            return addCardViewModel;
        }

        public async Task<ContinueRoundResponseViewModel> ContinueRound(long gameId, bool blackJackDangerContinue = false)
        {
            var logs = new List<Log>();
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            if (blackJackDangerContinue)
            {
                human.BetPayCoefficient = BetValueHelper.DefaultCoefficient;
            }

            List<Card> cardsOnHands = GetCardsOnHands(players.ToList());
            List<Card> deck = await ResumeDeck(cardsOnHands);
            
            players.Remove(human);
            await DistributeSecondCards(players, deck, logs);

            players.Remove(dealer);
            players.Add(human);
            _gamePlayerProvider.DefinePayCoefficientsAfterRoundContinue(players, dealer, logs);
            players.Add(dealer);
            await _gamePlayerRepository.UpdateManyAfterContinueRound(players);

            await _gameRepository.UpdateStage(gameId, StageHelper.SecondCardsDistribution);
            logs.Add(new Log() { GameId = gameId, Message = LogHelper.GameStageChanged(StageHelper.SecondCardsDistribution) });
            await _logRepository.CreateMany(logs);

            players.Remove(human);
            players.Remove(dealer);
            string humanRoundResult = _gamePlayerProvider.GetHumanRoundResult(human.BetPayCoefficient);
            ContinueRoundResponseViewModel continueRoundResponseViewModel = CustomMapper.GetContinueRoundResponseViewModel(players, gameId, humanRoundResult);
            return continueRoundResponseViewModel;
        }

        public async Task<ContinueRoundResponseViewModel> ResumeAfterContinueRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            string humanRoundResult = _gamePlayerProvider.GetHumanRoundResult(human.BetPayCoefficient);
            ContinueRoundResponseViewModel continueRoundResponseViewModel = CustomMapper.GetContinueRoundResponseViewModel(players, gameId, humanRoundResult);
            return continueRoundResponseViewModel;
        }

        public async Task EndRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            players.Remove(dealer);
            _gamePlayerProvider.PayBets(players, dealer);

            IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetAllByGameId(gameId);
            await _playerCardRepository.DeleteMany(playerCards);
            await _gameRepository.UpdateStage(gameId, StageHelper.RoundStart);

            players.Add(dealer);
            foreach(var gamePlayer in players)
            {
                gamePlayer.RoundScore = GameValueHelper.Zero;
                gamePlayer.CardAmount = GameValueHelper.Zero;
            }

            await _gamePlayerRepository.UpdateMany(players);
            await _gamePlayerRepository.DeleteBotsWithZeroScore(gameId);
        }
        
        public async Task EndGame(EndGameViewModel GameLogicEndGameView)
        {
            await _gameRepository.UpdateResult(GameLogicEndGameView.GameId, GameLogicEndGameView.Result);
            await _gamePlayerRepository.DeleteAllByGameId(GameLogicEndGameView.GameId);
        }

        private bool IsBlackJackChoice(GamePlayer human)
        {
            bool blackJackChoice = false;

            if (human.BetPayCoefficient == BetValueHelper.WinCoefficient)
            {
                blackJackChoice = true;
            }

            return blackJackChoice;
        }

        private async Task DistributeFirstCards(IEnumerable<GamePlayer> gamePlayers, List<Card> deck)
        {
            var logs = new List<Log>();
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
            await _logRepository.CreateMany(logs);
        }
        
        private async Task DistributeSecondCards(IEnumerable<GamePlayer> players, List<Card> deck, List<Log> logs)
        {
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in players)
            {
                AddSecondCardsToBot(gamePlayer, deck, logs, playerCards);
            }

            await _playerCardRepository.CreateMany(playerCards);
            await _logRepository.CreateMany(logs);
            logs.Clear();
        }

        private void AddSecondCardsToBot(GamePlayer gamePlayer, List<Card> deck, List<Log> logs, List<PlayerCard> playerCards)
        {
            if(_gamePlayerProvider.IsEnoughCardsForBot(gamePlayer.RoundScore))
            {
                return;
            }

            PlayerCard addedPlayerCard = AddCardToPlayer(gamePlayer, deck, logs);
            playerCards.Add(addedPlayerCard);
            gamePlayer.PlayerCards.Add(addedPlayerCard);
            gamePlayer.CardAmount++;
            gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);

            AddSecondCardsToBot(gamePlayer, deck, logs, playerCards);
        }
        
        private PlayerCard AddCardToPlayer(GamePlayer gamePlayer, List<Card> deck, List<Log> logs)
        {
            Card card = deck.First();
            deck.Remove(card);
            var playerCard = new PlayerCard() { GamePlayerId = gamePlayer.Id, CardId = card.Id, Card = card };
            var logFirstCard = new Log() { 
                GameId = gamePlayer.GameId,
                Message = LogHelper.CardAdded(card.Id, card.Name, CardToStringHelper.Convert(card), ((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name)
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

        private async Task<List<Card>> ResumeDeck(IEnumerable<Card> cardsOnHands)
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            deck.Except(cardsOnHands);
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

                if (cardFirst != cardSecond)
                {
                    glass = shuffledCards[cardFirst];
                    shuffledCards[cardFirst] = shuffledCards[cardSecond];
                    shuffledCards[cardSecond] = glass;
                }
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

        private List<Card> GetCardsOnHands(List<GamePlayer> players)
        {
            var cardsOnHands = new List<Card>();

            foreach(GamePlayer player in players)
            {
                cardsOnHands.AddRange(player.PlayerCards.ConvertAll((playerCard) => { return playerCard.Card; }));
            }

            return cardsOnHands;
        }
    }
}