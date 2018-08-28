using System.Collections.Generic;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class InitialDeck
    {
        public static readonly List<Card> Cards;

        static InitialDeck()
        {
            Cards = new List<Card>();

            int cardType = 0;
            int cardCount = 0;

            CardInitialization(cardType, cardCount);
        }

        static void CardInitialization(int cardType, int cardCount)
        {
            if (cardType >= CardValue.CardTypeAmount)
            {
                return;
            }

            Card card; 

            for (int cardName = (int)CardName.Two; cardName <= (int)CardName.Ace; cardName++)
            {
                if (cardName == (int)CardName.Ten)
                {
                    continue;
                }

                card = new Card () { Id = cardCount++, CardName = (CardName)cardName, CardType = (CardType)cardType};
                Cards.Add(card);
            }

            card = new Card { Id = cardCount++, CardName = CardName.Ten, CardType = (CardType)cardType };
            Cards.Add(card);
            card = new Card { Id = cardCount++, CardName = CardName.Jack, CardType = (CardType)cardType };
            Cards.Add(card);
            card = new Card { Id = cardCount++, CardName = CardName.Dame, CardType = (CardType)cardType };
            Cards.Add(card);
            card = new Card { Id = cardCount++, CardName = CardName.King, CardType = (CardType)cardType };
            Cards.Add(card);

            cardType++;
            CardInitialization(cardType, cardCount);
        }
    }
}
