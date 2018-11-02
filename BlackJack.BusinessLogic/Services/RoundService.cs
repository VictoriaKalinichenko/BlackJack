﻿using AutoMapper;
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
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            await _gameRepository.UpdateRoundResult(gameId, string.Empty);

            await RemoveCards(players, gameId);
            await DistributeCards(players, CardValue.TwoCardsPerPlayer);
            players.ForEach((player) =>
            {
                player.CardScore = CountCardScore(player.PlayerCards);
            });
            await _gamePlayerRepository.UpdateMany(players);
            await _historyMessageManager.AddMessagesForStartRound(players, gameId);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }

            StartRoundView startRoundView = CustomMapper.GetStartRoundView(players, gameId, canTakeCard);
            return startRoundView;
        }
        
        public async Task<AddCardRoundView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetHumanByGameId(gameId);

            Card card = (await _cardRepository.GetSpecifiedAmount(CardValue.OneCardPerPlayer)).First();
            PlayerCard addedPlayerCard = CustomMapper.GetPlayerCard(human, card);
            human.PlayerCards.Add(addedPlayerCard);
            await _playerCardRepository.Create(addedPlayerCard);

            human.CardScore = CountCardScore(human.PlayerCards);
            await _gamePlayerRepository.Update(human);

            AddCardRoundView addCardRoundView = Mapper.Map<GamePlayer, AddCardRoundView>(human);
            addCardRoundView.CanTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                addCardRoundView.CanTakeCard = false;
            }

            return addCardRoundView;
        }

        public async Task<ContinueRoundView> Continue(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            List<PlayerCard> createdPlayerCards = await DistributeCards(players, CardValue.OneCardPerPlayer, false);
            players.ForEach((player) =>
            {
                player.CardScore = CountCardScore(player.PlayerCards);
            });
            await _gamePlayerRepository.UpdateMany(players);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            string roundResult = GetRoundResult(human.CardScore, dealer.CardScore);
            await _gameRepository.UpdateRoundResult(gameId, roundResult);

            await _historyMessageManager.AddMessagesForContinueRound(players, createdPlayerCards, roundResult, gameId);

            ContinueRoundView continueRoundView = CustomMapper.GetContinueRoundView(players, gameId, roundResult);
            return continueRoundView;
        }

        public async Task<RestoreRoundView> Restore(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }
            
            RestoreRoundView restoreRoundView = CustomMapper.GetRestoreRoundView(players, gameId, canTakeCard);
            return restoreRoundView;
        }

        private async Task RemoveCards(List<GamePlayer> players, long gameId)
        {
            if (players.All(m => m.PlayerCards.Count() == 0))
            {
                return;
            }

            await _playerCardRepository.DeleteByGameId(gameId);
        }

        private async Task<List<PlayerCard>> DistributeCards(List<GamePlayer> players, int cardAmountPerPlayer, bool humanNeedsCards = true)
        {
            var createdPlayerCards = new List<PlayerCard>();
            int cardAmount = players.Count() * cardAmountPerPlayer;
            List<Card> deck = await _cardRepository.GetSpecifiedAmount(cardAmount);

            foreach (GamePlayer player in players)
            {
                if (humanNeedsCards || !(player.Player.Type == PlayerType.Human))
                {
                    List<Card> cards = PopCardsFromDeck(deck, cardAmountPerPlayer);
                    List<PlayerCard> addedPlayerCards = CustomMapper.GetPlayerCards(player, cards);
                    player.PlayerCards = addedPlayerCards;
                    createdPlayerCards.AddRange(addedPlayerCards);
                }
            }

            await _playerCardRepository.CreateMany(createdPlayerCards);
            return createdPlayerCards;
        }
        
        private string GetRoundResult(int humanScore, int dealerScore)
        {
            string roundResult = GameMessage.Lose;
            
            if (humanScore > dealerScore && humanScore <= CardValue.MaxCardScore 
                && dealerScore <= CardValue.MaxCardScore)
            {
                roundResult = GameMessage.Win;
            }

            if (humanScore == dealerScore && humanScore <= CardValue.MaxCardScore
                && dealerScore <= CardValue.MaxCardScore)
            {
                roundResult = GameMessage.Equal;
            }

            return roundResult;
        }

        private List<Card> PopCardsFromDeck(List<Card> deck, int cardAmount)
        {
            List<Card> cards = deck.GetRange(CardValue.FirstItemIndex, cardAmount);
            deck.RemoveRange(CardValue.FirstItemIndex, cardAmount);
            return cards;
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