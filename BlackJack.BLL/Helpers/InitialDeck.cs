using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Helpers
{
    public static class InitialDeck
    {
        public static readonly List<Card> Cards;

        static InitialDeck()
        {
            string[] cardNames = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Dame", "King", "Ace" };
            string[] cardTypes = new string[] { "Spades", "Clubs", "Hearts", "Diamonds" };

            int Count = 0;
            for (int i = 0; i < cardNames.Length; i++)
            {
                for (int j = 0; j < cardTypes.Length; j++)
                {
                    Card card = new Card();
                    card.Id = ++Count;
                    card.Name = cardNames[i] + " " + cardTypes[j];
                    card.Value = 10;

                    if (i == 12)
                    {
                        card.Value = 11;
                    }

                    if (i < 9)
                    {
                        card.Value = i + 2;
                    }

                    Cards.Add(card);
                }
            }
        }
    }
}
