using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.Entity;

namespace BlackJack.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private DataBaseContext db;
        
        public CardRepository(DataBaseContext context)
        {
            db = context;
        }



        public IEnumerable<Card> GetAll()
        {
            IEnumerable<Card> cards;
            cards = db.Cards;
            return cards;
        }

        public Card Get(int Id)
        {
            Card card;
            card = db.Cards.Find(Id);
            return card;
        }
    }
}
