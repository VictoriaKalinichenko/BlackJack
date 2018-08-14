using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers.RandomizeProviders;

namespace BlackJack.BLL.Providers.DeckProviders
{
    public class DeckProvider : IDeckProvider
    {
        public List<Card> CreateDeck()
        {
            List<Card> cards;

            cards = InitialDeck.Cards;

            return cards;
        }

        public Card TakeCardFromDeck(List<Card> cards)
        {
            Card card;

            IRandomizeProvider randomize = new RandomizeProvider();
            card = cards[randomize.CardIdSelection(cards.Count)];
            cards.Remove(card);

            return card;
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
