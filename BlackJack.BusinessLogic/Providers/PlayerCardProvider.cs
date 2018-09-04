using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Interfaces;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<int> AddingCardToPlayer(int gamePlayerId, List<int> deck)
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
                return cardId;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public int CardScoreCount(IEnumerable<PlayerCard> playerCards)
        {
            try
            {
                int roundScore = 0;
                
                List<Card> cards = new List<Card>();
                foreach (PlayerCard playerCard in playerCards)
                {
                    Card card = InitialDeck.Cards.Where(m => m.Id == playerCard.CardId).First();
                    int cardScore = (int)card.CardName;
                    if (cardScore > (int)CardName.Ace)
                    {
                        cardScore = (int)CardName.Ten;
                    }

                    roundScore += cardScore;
                    cards.Add(card);
                }
                
                int aceCount = cards
                    .Where(m => m.CardName == CardName.Ace)
                    .Count();

                for (; aceCount > 0 && roundScore > CardValue.CardBjScore;)
                {
                    aceCount--;
                    roundScore -= (int)CardName.Ten;
                }
                
                return roundScore;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
