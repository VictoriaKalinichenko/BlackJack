using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
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

        private readonly IGamePlayerManager _gamePlayerManager;
        private readonly IHistoryMessageManager _historyMessageManager;

        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, 
            IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            ICardRepository cardRepository, IGamePlayerManager gamePlayerManager, IHistoryMessageManager historyMessageManager)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _cardRepository = cardRepository;
            _gamePlayerManager = gamePlayerManager;
            _historyMessageManager = historyMessageManager;
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

        public async Task<StartRoundResponseViewModel> StartRound(StartRoundRequestViewModel startRoundRequestViewModel)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(startRoundRequestViewModel.GameId)).ToList();
            _gamePlayerManager.CreateBets(players, startRoundRequestViewModel.Bet);
            await DistributeFirstCards(players);
            _gamePlayerManager.DefinePayCoefficientsAfterRoundStart(players);
            await _gamePlayerRepository.UpdateMany(players);
            await _gameRepository.UpdateStage(startRoundRequestViewModel.GameId, GameStage.StartRound);
            await _historyMessageManager.AddStartRoundMessages(players, startRoundRequestViewModel.GameId);

            StartRoundResponseViewModel startRoundResponseViewModel = GetStartRoundResponse(players);
            return startRoundResponseViewModel;
        }
        
        public async Task<StartRoundResponseViewModel> ResumeAfterStartRound(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            StartRoundResponseViewModel startRoundResponseViewModel = GetStartRoundResponse(players);
            return startRoundResponseViewModel;
        }
        
        public async Task<AddCardGameView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetWithCards(gameId, (int)PlayerType.Human);
            await AddOneCardToHuman(human, gameId);
            await _gamePlayerRepository.UpdateAddingCard(human);

            AddCardGameView addCardViewModel = Mapper.Map<GamePlayer, AddCardGameView>(human);
            addCardViewModel.CanTakeCard = true;

            if (human.RoundScore >= CardValueHelper.BlackJackScore)
            {
                addCardViewModel.CanTakeCard = false;
            }

            return addCardViewModel;
        }

        public async Task<ContinueRoundResponseViewModel> ContinueRound(RequestContinueRoundGameView continueRoundRequestViewModel)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(continueRoundRequestViewModel.GameId)).ToList();

            if (continueRoundRequestViewModel.ContinueRound)
            {
                players.Where(m => m.Player.Type == PlayerType.Human).First().BetPayCoefficient = BetValueHelper.DefaultCoefficient;
            }

            List<PlayerCard> playerCardsInserted = await DistributeSecondCards(players, continueRoundRequestViewModel.GameId);
            _gamePlayerManager.DefinePayCoefficientsAfterRoundContinue(players);
            await _gamePlayerRepository.UpdateManyAfterContinueRound(players);
            await _gameRepository.UpdateStage(continueRoundRequestViewModel.GameId, GameStage.ContinueRound);
            await _historyMessageManager.AddContinueRoundMessages(players, playerCardsInserted, continueRoundRequestViewModel.GameId);

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
            _gamePlayerManager.PayBets(players);
            await RemoveCards(players, gameId);
            await _gamePlayerRepository.UpdateMany(players);
            await _gamePlayerRepository.DeleteBotsWithZeroScore(gameId);
            await _gameRepository.UpdateStage(gameId, GameStage.InitRound);
        }
        
        public async Task EndGame(EndGameView GameLogicEndGameView)
        {
            await _gameRepository.UpdateResult(GameLogicEndGameView.GameId, GameLogicEndGameView.Result);
            await _gamePlayerRepository.DeleteAllByGameId(GameLogicEndGameView.GameId);
        }
        
        private async Task DistributeFirstCards(IEnumerable<GamePlayer> gamePlayers)
        {
            List<Card> deck = (await _cardRepository.GetAll()).ToList();
            deck = ShuffleDeck(deck);
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.PlayerCards = new List<PlayerCard>();
                PlayerCard firstCard = AddCardToPlayer(gamePlayer, deck);
                PlayerCard secondCard = AddCardToPlayer(gamePlayer, deck);
                playerCards.AddRange(gamePlayer.PlayerCards);
            }

            await _playerCardRepository.CreateMany(playerCards);
        }

        private async Task AddOneCardToHuman(GamePlayer human, long gameId)
        {
            List<Card> deck = (await _cardRepository.ResumeDeck(gameId)).ToList();
            deck = ShuffleDeck(deck);
            PlayerCard addedPlayerCard = AddCardToPlayer(human, deck);
            await _playerCardRepository.Create(addedPlayerCard);
        }

        private async Task<List<PlayerCard>> DistributeSecondCards(IEnumerable<GamePlayer> players, long gameId)
        {
            List<Card> deck = (await _cardRepository.ResumeDeck(gameId)).ToList();
            deck = ShuffleDeck(deck);
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in players)
            {
                if (!(gamePlayer.Player.Type == PlayerType.Human))
                {
                    await AddSecondCardsToBot(gamePlayer, deck, playerCards);
                }
            }

            await _playerCardRepository.CreateMany(playerCards);
            return playerCards;
        }

        private async Task AddSecondCardsToBot(GamePlayer gamePlayer, List<Card> deck, List<PlayerCard> playerCards)
        {
            if(gamePlayer.RoundScore >= CardValueHelper.BotEnoughScore)
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
        
        private StartRoundResponseViewModel GetStartRoundResponse(List<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();

            bool canTakeCard = true;
            if (human.RoundScore >= CardValueHelper.BlackJackScore)
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
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            string humanRoundResult = _gamePlayerManager.GetHumanRoundResult(human.BetPayCoefficient);
            ContinueRoundResponseViewModel continueRoundResponseViewModel =
                CustomMapper.GetContinueRoundResponseViewModel(players, human.GameId, humanRoundResult);
            return continueRoundResponseViewModel;
        }

        private async Task RemoveCards(List<GamePlayer> players, long gameId)
        {
            await _playerCardRepository.DeleteAllByGameId(gameId);

            foreach (var gamePlayer in players)
            {
                gamePlayer.RoundScore = GameValueHelper.Zero;
                gamePlayer.CardAmount = GameValueHelper.Zero;
            }
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
                int cardScore = (int)playerCard.Card.Rank;
                if (cardScore > (int)CardRank.Ace)
                {
                    cardScore = (int)CardRank.Ten;
                }
                roundScore += cardScore;
            }
            
            for (int aceCount = playerCards.Where(m => m.Card.Rank == CardRank.Ace).Count(); 
                aceCount > 0 && roundScore > CardValueHelper.BlackJackScore; 
                aceCount--, roundScore -= (int)CardRank.Ten);

            return roundScore;
        }
    }
}