using AutoMapper;
using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.Round;
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
        
        private readonly IHistoryMessageManager _historyMessageManager;

        public RoundService(IPlayerRepository playerRepository, IGameRepository gameRepository, 
            IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            ICardRepository cardRepository, IHistoryMessageManager historyMessageManager)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _cardRepository = cardRepository;
            _historyMessageManager = historyMessageManager;
        }

        public async Task<StartRoundView> Start(long gameId)
        {  
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllByGameId(gameId)).ToList();
            await _gameRepository.UpdateRoundResult(gameId, string.Empty);

            players = await RemoveCards(players, gameId);
            players = await DistributeTwoCardsPerPlayer(players);
            
            await _historyMessageManager.AddMessagesForStartRound(players, gameId);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = CanTakeCard(human);

            StartRoundView startRoundView = CustomMapper.GetStartRoundView(players, gameId, canTakeCard);
            return startRoundView;
        }
        
        public async Task<AddCardRoundView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetHumanByGameId(gameId);

            long cardId = GetRandomCardId();
            Card card = await _cardRepository.Get(cardId);
            PlayerCard createdPlayerCard = AddCardToPlayer(human, card);
            await _playerCardRepository.Create(createdPlayerCard);

            AddCardRoundView addCardRoundView = Mapper.Map<GamePlayer, AddCardRoundView>(human);

            addCardRoundView.CanTakeCard = true;
            int cardScore = CountCardScore(human.PlayerCards);
            if (cardScore >= CardValue.MaxCardScore)
            {
                addCardRoundView.CanTakeCard = false;
            }

            return addCardRoundView;
        }

        public async Task<ContinueRoundView> Continue(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllByGameId(gameId)).ToList();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();

            List<PlayerCard> createdPlayerCards = await AddExtraCardsForDealer(dealer);
            List<PlayerCard> createdExtraPlayerCardsForBots = await AddExtraCardsForBots(players);
            createdPlayerCards.AddRange(createdExtraPlayerCardsForBots);
            await _playerCardRepository.CreateMany(createdPlayerCards);

            string roundResult = GetRoundResult(human, dealer);
            await _gameRepository.UpdateRoundResult(gameId, roundResult);

            await _historyMessageManager.AddMessagesForContinueRound(players, createdPlayerCards, gameId);

            ContinueRoundView continueRoundView = CustomMapper.GetContinueRoundView(players, gameId, roundResult);
            return continueRoundView;
        }

        public async Task<RestoreRoundView> Restore(long gameId)
        {
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllByGameId(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = CanTakeCard(human);

            StartRoundView startRoundView = CustomMapper.GetStartRoundView(players, gameId, canTakeCard);
            RestoreRoundView restoreRoundView = Mapper.Map<StartRoundView, RestoreRoundView>(startRoundView);
            return restoreRoundView;
        }

        private async Task<List<GamePlayer>> RemoveCards(List<GamePlayer> players, long gameId)
        {
            List<GamePlayer> returnedPlayers = players;

            if (returnedPlayers.All(m => m.PlayerCards.Count() == 0))
            {
                return returnedPlayers;
            }

            await _playerCardRepository.DeleteAllByGameId(gameId);
            return returnedPlayers;
        }

        private async Task<List<GamePlayer>> DistributeTwoCardsPerPlayer(List<GamePlayer> players)
        {
            List<GamePlayer> returnedPlayers = players;
            var createdPlayerCards = new List<PlayerCard>();

            int cardAmount = returnedPlayers.Count() * CardValue.TwoCardsPerPlayer;
            List<Card> deck = (await _cardRepository.GetSpecifiedAmount(cardAmount)).ToList();

            foreach (GamePlayer player in returnedPlayers)
            {
                player.PlayerCards = new List<PlayerCard>();

                Card card = TakeCardFromDeck(deck);
                PlayerCard createdPlayerCard = AddCardToPlayer(player, card);

                card = TakeCardFromDeck(deck);
                createdPlayerCard = AddCardToPlayer(player, card);

                createdPlayerCards.AddRange(player.PlayerCards);
            }

            await _playerCardRepository.CreateMany(createdPlayerCards);

            return returnedPlayers;
        }

        private async Task<List<PlayerCard>> AddExtraCardsForDealer(GamePlayer dealer)
        {
            List<Card> deck = (await _cardRepository.GetSpecifiedAmount(CardValue.MaxPossibleCardAmountForPlayer)).ToList();
            var createdPlayerCards = new List<PlayerCard>();

            int cardScore = CountCardScore(dealer.PlayerCards);
            for (int iterator = deck.Count(); iterator > 0 && cardScore < CardValue.EnoughDealerCardScore; iterator--)
            {
                Card card = TakeCardFromDeck(deck);
                PlayerCard createdPlayerCard = AddCardToPlayer(dealer, card);
                cardScore = CountCardScore(dealer.PlayerCards);
                createdPlayerCards.Add(createdPlayerCard);
            }
            
            return createdPlayerCards;
        }

        private async Task<List<PlayerCard>> AddExtraCardsForBots(IEnumerable<GamePlayer> players)
        {
            int cardAmount = players.Where(m => m.Player.Type == PlayerType.Bot).Count();
            List<Card> deck = (await _cardRepository.GetSpecifiedAmount(cardAmount)).ToList();
            var createdPlayerCards = new List<PlayerCard>();

            foreach (GamePlayer player in players)
            {
                if (player.Player.Type == PlayerType.Bot)
                {
                    Card card = TakeCardFromDeck(deck);
                    PlayerCard createdPlayerCard = AddCardToPlayer(player, card);
                    createdPlayerCards.Add(createdPlayerCard);
                }
            }
            
            return createdPlayerCards;
        }

        private bool CanTakeCard(GamePlayer player)
        {
            bool canTakeCard = true;

            int humanScore = CountCardScore(player.PlayerCards);
            if (humanScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }

            return canTakeCard;
        }

        private string GetRoundResult(GamePlayer human, GamePlayer dealer)
        {
            string roundResult = GameMessage.Lose;

            int humanScore = CountCardScore(human.PlayerCards);
            int dealerScore = CountCardScore(dealer.PlayerCards);

            if (humanScore > dealerScore && humanScore <= CardValue.MaxCardScore)
            {
                roundResult = GameMessage.Win;
            }

            if (humanScore == dealerScore && humanScore <= CardValue.MaxCardScore)
            {
                roundResult = GameMessage.Equal;
            }

            return roundResult;
        }

        private Card TakeCardFromDeck(List<Card> deck)
        {
            Card card = deck.First();
            deck.Remove(card);
            return card;
        }
        
        private PlayerCard AddCardToPlayer(GamePlayer player, Card card)
        {
            PlayerCard playerCard = CustomMapper.GetPlayerCard(player, card);
            player.PlayerCards.Add(playerCard);
            return playerCard;
        }

        private int CountCardScore(List<PlayerCard> playerCards)
        {
            int roundScore = 0;

            int aceCount = 0;
            foreach (PlayerCard playerCard in playerCards)
            {
                if (playerCard.Card.Worth == CardValue.AceFullWorth)
                {
                    aceCount++;
                }
                
                if (playerCard.Card.Worth != CardValue.AceFullWorth)
                {
                    roundScore += playerCard.Card.Worth;
                }
            }
            
            for (int iterator = aceCount; iterator > 0; iterator--)
            {
                int aceWorth = CardValue.AceFullWorth;
                if (roundScore >= CardValue.MaxCardScore)
                {
                    aceWorth = CardValue.AceOnePointWorth;
                }
                roundScore += aceWorth;
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