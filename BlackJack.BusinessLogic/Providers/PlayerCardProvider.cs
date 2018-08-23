using System.Collections.Generic;
using System.Linq;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Providers
{
    public class PlayerCardProvider : IPlayerCardProvider
    {
        private readonly IPlayerCardRepository _playerCardRepository;

        public PlayerCardProvider()
        {
            _playerCardRepository = new PlayerCardRepository();
        }

        public string PlayerCardToCardString(PlayerCard playerCard)
        {
            string result;
            result = InitialDeck._cards[playerCard.CardId].ToString();
            return result;
        }

        public List<string> GetCardsStringList(List<PlayerCard> playerCards)
        {
            List<string> cardsStringList = new List<string>();
            cardsStringList = playerCards.ConvertAll(PlayerCardToCardString);
            return cardsStringList;
        }

        public void AddingCardToPlayer(int gamePlayerId, List<int> deck)
        {
            int cardId = deck.First();
            deck.Remove(cardId);

            PlayerCard playerCard = new PlayerCard()
            {
                CardId = cardId,
                GamePlayerId = gamePlayerId
            };
            _playerCardRepository.Create(playerCard);
        }

        public int CardScoreCount(List<PlayerCard> playerCards)
        {
            int roundScore;

            List<Card> cards = new List<Card>();
            foreach (PlayerCard playerCard in playerCards)
            {
                Card card = InitialDeck._cards.Where(m => m.Id == playerCard.CardId).First();
                cards.Add(card);
            }

            int count = cards.Sum(m => (int)m.CardName);
            int AceCount = cards
                .Where(m => m.CardName == CardName.Ace)
                .Count();

            for (; AceCount > 0 && count > CardValue._cardBjScore;)
            {
                AceCount--;
                count -= (int)CardName.Ten;
            }

            roundScore = count;
            return roundScore;
        }
    }
}
