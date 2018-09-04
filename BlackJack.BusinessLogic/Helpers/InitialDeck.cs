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

            for (int cardName = (int)CardName.Two; cardName <= (int)CardName.King; cardName++)
            {
                card = new Card () { Id = cardCount++, CardName = (CardName)cardName, CardType = (CardType)cardType};
                Cards.Add(card);
            }

            cardType++;
            CardInitialization(cardType, cardCount);
        }
    }
}
