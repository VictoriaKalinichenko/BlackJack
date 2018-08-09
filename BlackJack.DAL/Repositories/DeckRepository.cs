using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.Entity;
using System.Data.Entity;


namespace BlackJack.DAL.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private DataBaseContext db;

        public DeckRepository(DataBaseContext context)
        {
            db = context;
        }



        public Deck Get(int Id)
        {
            Deck Deck;
            Deck = db.Decks.Find(Id);
            return Deck;
        }

        public void Create(Deck obj)
        {
            db.Decks.Add(obj);
        }

        public void Update(Deck obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            Deck Deck = db.Decks.Find(Id);
            if (Deck != null)
                db.Decks.Remove(Deck);
        }
    }
}
