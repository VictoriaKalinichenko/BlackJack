using AutoMapper;
using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class RoundService : IRoundService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ICardRepository _cardRepository;

        private readonly IGamePlayerManager _gamePlayerManager;
        private readonly IHistoryMessageManager _historyMessageManager;

        public RoundService(IPlayerRepository playerRepository, IGameRepository gameRepository, 
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

        public async Task<ResponseStartRoundView> Start(RequestStartRoundView requestStartRoundView)
        {
            bool betIsCorrect = await ValidateBet(requestStartRoundView.Bet, requestStartRoundView.GamePlayerId);
            if (!betIsCorrect)
            {
                return null;
            }
                
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithoutCards(requestStartRoundView.GameId)).ToList();
            _gamePlayerManager.CreateBets(players, requestStartRoundView.Bet);
            await DistributeFirstCards(players);
            _gamePlayerManager.DefinePayCoefficientsAfterRoundStart(players);
            await _gamePlayerRepository.UpdateMany(players);
            await _gameRepository.UpdateStage(requestStartRoundView.GameId, GameStage.StartRound);
            await _historyMessageManager.AddStartRoundMessages(players, requestStartRoundView.GameId);

            ResponseStartRoundView responseStartRoundView = GetStartRoundResponse(players);
            return responseStartRoundView;
        }
        
        public async Task<ResumeAfterStartRoundView> ResumeAfterStart(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            ResponseStartRoundView responseStartRoundView = GetStartRoundResponse(players);
            ResumeAfterStartRoundView resumeAfterStartRoundView = Mapper.Map<ResponseStartRoundView, ResumeAfterStartRoundView>(responseStartRoundView);
            return resumeAfterStartRoundView;
        }
        
        public async Task<AddCardRoundView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetWithCards(gameId, (int)PlayerType.Human);
            await AddOneCardToHuman(human, gameId);
            await _gamePlayerRepository.UpdateAddingCard(human);

            AddCardRoundView addCardRoundView = Mapper.Map<GamePlayer, AddCardRoundView>(human);
            addCardRoundView.CanTakeCard = true;

            if (human.RoundScore >= CardValue.BlackJackScore)
            {
                addCardRoundView.CanTakeCard = false;
            }

            return addCardRoundView;
        }

        public async Task<ResponseContinueRoundView> Continue(RequestContinueRoundView requestContinueRoundView)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(requestContinueRoundView.GameId)).ToList();

            if (requestContinueRoundView.ContinueRound)
            {
                players.Where(m => m.Player.Type == PlayerType.Human).First().BetPayCoefficient = BetValue.DefaultCoefficient;
            }

            List<PlayerCard> playerCardsInserted = await DistributeSecondCards(players, requestContinueRoundView.GameId);
            _gamePlayerManager.DefinePayCoefficientsAfterRoundContinue(players);
            await _gamePlayerRepository.UpdateManyAfterContinueRound(players);
            await _gameRepository.UpdateStage(requestContinueRoundView.GameId, GameStage.ContinueRound);
            await _historyMessageManager.AddContinueRoundMessages(players, playerCardsInserted, requestContinueRoundView.GameId);

            ResponseContinueRoundView responseContinueRoundView = GetContinueRoundResponse(players);
            return responseContinueRoundView;
        }

        public async Task<ResumeAfterContinueRoundView> ResumeAfterContinue(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllWithCards(gameId)).ToList();
            ResponseContinueRoundView responseContinueRoundView = GetContinueRoundResponse(players);
            ResumeAfterContinueRoundView resumeAfterContinueRoundView = Mapper.Map<ResponseContinueRoundView, ResumeAfterContinueRoundView>(responseContinueRoundView);
            return resumeAfterContinueRoundView;
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
        
        public async Task EndGame(EndGameRoundView endGameRoundView)
        {
            await _gameRepository.UpdateResult(endGameRoundView.GameId, endGameRoundView.Result);
            await _gamePlayerRepository.DeleteAllByGameId(endGameRoundView.GameId);
        }

        private async Task<bool> ValidateBet(int bet, long gamePlayerId)
        {
            bool isCorrect = true;
            int score = await _gamePlayerRepository.GetScoreById(gamePlayerId);

            if (bet > score || bet <= 0)
            {
                isCorrect = false;
            }

            return isCorrect;
        }

        private async Task DistributeFirstCards(IEnumerable<GamePlayer> gamePlayers)
        {
            int cardAmount = gamePlayers.Count() * CardValue.BlackJackAmount;
            List<Card> deck = (await _cardRepository.GetSpecifiedAmount(cardAmount)).ToList();
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.PlayerCards = new List<PlayerCard>();
                Card firstCard = TakeCardFromDeck(deck);
                PlayerCard firstPlayerCard = AddCardToPlayer(gamePlayer, firstCard);
                Card secondCard = TakeCardFromDeck(deck);
                PlayerCard secondPlayerCard = AddCardToPlayer(gamePlayer, secondCard);
                playerCards.AddRange(gamePlayer.PlayerCards);
            }

            await _playerCardRepository.CreateMany(playerCards);
        }

        private async Task AddOneCardToHuman(GamePlayer human, long gameId)
        {
            long cardId = GetRandomCardId();
            Card card = await _cardRepository.Get(cardId);
            PlayerCard addedPlayerCard = AddCardToPlayer(human, card);
            await _playerCardRepository.Create(addedPlayerCard);
        }

        private async Task<List<PlayerCard>> DistributeSecondCards(IEnumerable<GamePlayer> players, long gameId)
        {
            int cardAmount = players.Count();
            List<Card> deck = (await _cardRepository.GetSpecifiedAmount(cardAmount)).ToList();
            var playerCards = new List<PlayerCard>();

            foreach (GamePlayer gamePlayer in players)
            {
                if (!(gamePlayer.Player.Type == PlayerType.Human) && 
                    gamePlayer.RoundScore < CardValue.BlackJackScore)
                {
                    Card card = TakeCardFromDeck(deck);
                    PlayerCard playerCard = AddCardToPlayer(gamePlayer, card);
                    playerCards.Add(playerCard);
                }
            }

            await _playerCardRepository.CreateMany(playerCards);
            return playerCards;
        }
        
        private PlayerCard AddCardToPlayer(GamePlayer gamePlayer, Card card)
        {
            PlayerCard playerCard = CustomMapper.GetPlayerCard(gamePlayer, card);
            gamePlayer.PlayerCards.Add(playerCard);
            gamePlayer.CardAmount++;
            gamePlayer.RoundScore = CountCardScore(gamePlayer.PlayerCards);
            return playerCard;
        }
        
        private ResponseStartRoundView GetStartRoundResponse(List<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();

            bool canTakeCard = true;
            if (human.RoundScore >= CardValue.BlackJackScore)
            {
                canTakeCard = false;
            }

            bool blackJackChoice = false;
            if (human.BetPayCoefficient == BetValue.WinCoefficient)
            {
                blackJackChoice = true;
            }

            ResponseStartRoundView responseStartRoundView =
                CustomMapper.GetResponseStartRoundView(players, human.GameId, canTakeCard, blackJackChoice);
            return responseStartRoundView;
        }

        private ResponseContinueRoundView GetContinueRoundResponse(List<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            string humanRoundResult = _gamePlayerManager.GetHumanRoundResult(human.BetPayCoefficient);
            ResponseContinueRoundView responseContinueRoundView =
                CustomMapper.GetResponseContinueRoundView(players, human.GameId, humanRoundResult);
            return responseContinueRoundView;
        }

        private async Task RemoveCards(List<GamePlayer> players, long gameId)
        {
            await _playerCardRepository.DeleteAllByGameId(gameId);

            foreach (var gamePlayer in players)
            {
                gamePlayer.RoundScore = 0;
                gamePlayer.CardAmount = 0;
            }
        }

        private Card TakeCardFromDeck(List<Card> deck)
        {
            Card card = deck.First();
            deck.Remove(card);
            return card;
        }

        private int CountCardScore(List<PlayerCard> playerCards)
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

            int aceCount = playerCards.Where(m => m.Card.Rank == CardRank.Ace).Count();
            for (; aceCount > 0 && roundScore > CardValue.BlackJackScore;)
            {
                aceCount--;
                roundScore -= (int)CardRank.Ten;
            }

            return roundScore;
        }

        private long GetRandomCardId()
        {
            long cardId;
            var random = new Random();
            cardId = random.Next(CardValue.MinId, CardValue.MaxId);
            return cardId;
        }
    }
}