using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Services;
using BlackJack.BLL.Services.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers
{
    public class CardDistributionProvider : ICardDistributionProvider
    {
        private IGameService GameService;

        public CardDistributionProvider()
        {
            GameService = new GameService();
        }
    
        public List<Card> CreateDeck()
        {
            List<Card> cards;

            cards = InitialDeck.Cards;

            return cards;
        }
        
        public int CardToIntConverter(Card card)
        {
            int id;

            id = card.Id;

            return id;
        }
        
        public List<Card> ShuffleDeck(List<Card> cards)
        {
            List<Card> shuffledCards = cards;

            Random random = new Random();
            int card1;
            int card2;
            Card glass;

            for (int i = 0; i < shuffledCards.Count; i++)
            {
                card1 = random.Next(shuffledCards.Count);
                card2 = random.Next(shuffledCards.Count);

                glass = shuffledCards[card1];
                shuffledCards[card1] = shuffledCards[card2];
                shuffledCards[card2] = glass;
            }

            return shuffledCards;
        }
    }
}
