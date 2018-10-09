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
        private readonly ICardRepository _cardRepository;
        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly ILogRepository _logRepository;

        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, 
            IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            IGamePlayerProvider gamePlayerProvider, ICardRepository cardRepository, ILogRepository logRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _cardRepository = cardRepository;
            _gamePlayerProvider = gamePlayerProvider;
            _logRepository = logRepository;
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
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(gameId)).ToList();
            _gamePlayerProvider.CreateBets(players, bet);
            await DistributeFirstCards(players);
            _gamePlayerProvider.DefinePayCoefficientsAfterRoundStart(players);
            await _gamePlayerRepository.UpdateMany(players);
            await _gameRepository.UpdateStage(gameId, (int)GameStage.StartRound);

            List<Log> logs = LogHelper.GetStartRoundLogs(players, gameId);
            await _logRepository.CreateMany(logs, ToStringHelper.GetTableName(typeof(Log)));

            StartRoundResponseViewModel startRoundResponseViewModel = GetStartRoundResponse(players);
            return startRoundResponseViewModel;
        }
        
        public async Task<StartRoundResponseViewModel> ResumeAfterStartRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            StartRoundResponseViewModel startRoundResponseViewModel = GetStartRoundResponse(players);
            return startRoundResponseViewModel;
        }
        
        public async Task<AddCardViewModel> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetWithCards(gameId, (int)PlayerType.Human);
            await AddOneCardToHuman(human, gameId);
            await _gamePlayerRepository.UpdateAddingCard(human);

            AddCardViewModel addCardViewModel = Mapper.Map<GamePlayer, AddCardViewModel>(human);
            addCardViewModel.CanTakeCard = true;

            if (human.RoundScore >= CardValueHelper.CardBlackJackScore)
            {
                addCardViewModel.CanTakeCard = false;
            }

            return addCardViewModel;
        }

        public async Task<ContinueRoundResponseViewModel> ContinueRound(long gameId, bool continueRound = false)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();

            if (continueRound)
            {
                players.Where(m => m.Player.Type == (int)PlayerType.Human).First().BetPayCoefficient = BetValueHelper.DefaultCoefficient;
            }

            List<PlayerCard> playerCardInserted = await DistributeSecondCards(players);
            _gamePlayerProvider.DefinePayCoefficientsAfterRoundContinue(players);
            await _gamePlayerRepository.UpdateManyAfterContinueRound(players);
            await _gameRepository.UpdateStage(gameId, (int)GameStage.ContinueRound);

            List<Log> logs = LogHelper.GetContinueRoundLogs(players, playerCardInserted, gameId);
            await _logRepository.CreateMany(logs, ToStringHelper.GetTableName(typeof(Log)));

            ContinueRoundResponseViewModel continueRoundResponseViewModel = GetContinueRoundResponse(players);
            return continueRoundResponseViewModel;
        }

        public async Task<ContinueRoundResponseViewModel> ResumeAfterContinueRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            ContinueRoundResponseViewModel continueRoundResponseViewModel = GetContinueRoundResponse(players);
            return continueRoundResponseViewModel;
        }

        public async Task EndRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(gameId)).ToList();
            _gamePlayerProvider.PayBets(players);
            await RemoveCards(players, gameId);
            await _gamePlayerRepository.UpdateMany(players);
            await _gamePlayerRepository.DeleteBotsWithZeroScore(gameId);
            await _gameRepository.UpdateStage(gameId, (int)GameStage.InitRound);
        }
        
        public async Task EndGame(EndGameViewModel GameLogicEndGameView)
        {
            await _gameRepository.UpdateResult(GameLogicEndGameView.GameId, GameLogicEndGameView.Result);
            await _gamePlayerRepository.DeleteAllByGameId(GameLogicEndGameView.GameId);
        }

        private StartRoundResponseViewModel GetStartRoundResponse(List<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();

            bool canTakeCard = true;
            if (human.RoundScore >= CardValueHelper.CardBlackJackScore)
            {
                canTakeCard = false;
            }

            bool blackJackChoice = false;
            if (human.BetPayCoefficient == BetValueHelper.WinCoefficient)
            {
                blackJackChoice = true;
            }

            StartRoundResponseViewModel startRoundResponseViewModel = 
                CustomMapper.GetStartRoundResponseViewModel(players, human.GameId, canTakeCard, blackJackChoice);
            return startRoundResponseViewModel;
        }

        private ContinueRoundResponseViewModel GetContinueRoundResponse(List<GamePlayer> players) 
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            string humanRoundResult = _gamePlayerProvider.GetHumanRoundResult(human.BetPayCoefficient);
            ContinueRoundResponseViewModel continueRoundResponseViewModel = 
                CustomMapper.GetContinueRoundResponseViewModel(players, human.GameId, humanRoundResult);
            return continueRoundResponseViewModel;
        }
        
        private async Task DistributeFirstCards(IEnumerable<GamePlayer> gamePlayers)
        {
            List<Card> deck = await CreateDeck();
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.PlayerCards = new List<PlayerCard>();

                Card card = TakeCardFromDeck(deck);
                PlayerCard firstCard = CustomMapper.GetPlayerCard(gamePlayer, card);
                gamePlayer.PlayerCards.Add(firstCard);

                card = TakeCardFromDeck(deck);
                PlayerCard secondCard = CustomMapper.GetPlayerCard(gamePlayer, card);
                gamePlayer.PlayerCards.Add(secondCard);

                gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
                gamePlayer.CardAmount = gamePlayer.PlayerCards.Count();
                playerCards.AddRange(gamePlayer.PlayerCards);
            }

            await _playerCardRepository.CreateMany(playerCards, ToStringHelper.GetTableName(typeof(PlayerCard)));
        }

        private async Task AddOneCardToHuman(GamePlayer human, long gameId)
        {
            IEnumerable<Card> cardsOnHands = await _playerCardRepository.GetCardsOnHands(gameId);
            List<Card> deck = await ResumeDeck(cardsOnHands);

            PlayerCard addedPlayerCard = AddCardToPlayer(human, deck);
            await _playerCardRepository.Create(addedPlayerCard);
        }

        private async Task<List<PlayerCard>> DistributeSecondCards(IEnumerable<GamePlayer> players)
        {
            List<Card> cardsOnHands = GetCardsOnHands(players.ToList());
            List<Card> deck = await ResumeDeck(cardsOnHands);
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in players)
            {
                if (!(gamePlayer.Player.Type == (int)PlayerType.Human))
                {
                    await AddSecondCardsToBot(gamePlayer, deck, playerCards);
                }
            }

            await _playerCardRepository.CreateMany(playerCards, ToStringHelper.GetTableName(typeof(PlayerCard)));
            return playerCards;
        }

        private async Task AddSecondCardsToBot(GamePlayer gamePlayer, List<Card> deck, List<PlayerCard> playerCards)
        {
            if(gamePlayer.RoundScore >= CardValueHelper.CardBotEnoughScore)
            {
                return;
            }

            PlayerCard addedPlayerCard = AddCardToPlayer(gamePlayer, deck);
            playerCards.Add(addedPlayerCard);
            await AddSecondCardsToBot(gamePlayer, deck, playerCards);
        }
        
        private PlayerCard AddCardToPlayer(GamePlayer gamePlayer, List<Card> deck)
        {
            Card card = TakeCardFromDeck(deck);
            PlayerCard playerCard = CustomMapper.GetPlayerCard(gamePlayer, card);
            gamePlayer.PlayerCards.Add(playerCard);
            gamePlayer.CardAmount++;
            gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
            return playerCard;
        }

        private async Task RemoveCards(List<GamePlayer> players, long gameId)
        {
            IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetAllByGameId(gameId);
            await _playerCardRepository.DeleteMany(playerCards, ToStringHelper.GetTableName(typeof(PlayerCard)));

            foreach (var gamePlayer in players)
            {
                gamePlayer.RoundScore = GameValueHelper.Zero;
                gamePlayer.CardAmount = GameValueHelper.Zero;
            }
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

        private Card TakeCardFromDeck(List<Card> deck)
        {
            Card card = deck.First();
            deck.Remove(card);
            return card;
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
            
            for (int aceCount = playerCards.Where(m => m.Card.Name == (int)CardName.Ace).Count(); 
                aceCount > 0 && roundScore > CardValueHelper.CardBlackJackScore; 
                aceCount--, roundScore -= (int)CardName.Ten);

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