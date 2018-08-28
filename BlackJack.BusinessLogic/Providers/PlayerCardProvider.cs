﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.ViewModels.Enums;
using NLog;

namespace BlackJack.BusinessLogic.Providers
{
    public class PlayerCardProvider : IPlayerCardProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IPlayerCardRepository _playerCardRepository;


        public PlayerCardProvider(IPlayerCardRepository playerCardRepository)
        {
            _playerCardRepository = playerCardRepository;
        }

        public string PlayerCardToCardString(PlayerCard playerCard)
        {
            try
            {
                string result = InitialDeck.Cards[playerCard.CardId].ToString();
                return result;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards)
        {
            try
            {
                var cardsStringList = playerCards.ToList().ConvertAll(PlayerCardToCardString);
                return cardsStringList;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task AddingCardToPlayer(int gamePlayerId, List<int> deck)
        {
            try
            {
                int cardId = deck.First();
                deck.Remove(cardId);

                PlayerCard playerCard = new PlayerCard()
                {
                    CardId = cardId,
                    GamePlayerId = gamePlayerId
                };
                await _playerCardRepository.Create(playerCard);
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public int CardScoreCount(IEnumerable<PlayerCard> playerCards)
        {
            try
            {
                int roundScore;

                List<Card> cards = new List<Card>();
                foreach (PlayerCard playerCard in playerCards)
                {
                    Card card = InitialDeck.Cards.Where(m => m.Id == playerCard.CardId).First();
                    cards.Add(card);
                }

                int count = cards.Sum(m => (int)m.CardName);
                int aceCount = cards
                    .Where(m => m.CardName == CardName.Ace)
                    .Count();

                for (; aceCount > 0 && count > CardValue.CardBjScore;)
                {
                    aceCount--;
                    count -= (int)CardName.Ten;
                }

                roundScore = count;
                return roundScore;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }
    }
}