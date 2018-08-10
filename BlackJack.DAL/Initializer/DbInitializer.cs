using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BlackJack.DAL.Context;
using BlackJack.Entity;

namespace BlackJack.DAL.Initializer
{
    public class DbInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext db)
        {
            string[] cardNames = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Dame", "King", "Ace" };
            string[] cardTypes = new string[] { "Spades", "Clubs", "Hearts", "Diamonds" };

            for (int i = 0; i < cardNames.Length; i++)
                for (int j = 0; j < cardTypes.Length; j++)
                {
                    Card card = new Card();
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

                    db.Cards.Add(card);
                }

            db.SaveChanges();
        }
    }
}
