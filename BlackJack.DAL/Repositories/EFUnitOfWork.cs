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
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataBaseContext db;
        private CardRepository cardRepository;
        private PlayerRepository playerRepository;
        private PlayerCardRepository playerCardRepository;

        public EFUnitOfWork()
        {
            db = new DataBaseContext();
        }


        public ICardRepository Cards
        {
            get
            {
                if (cardRepository == null)
                {
                    cardRepository = new CardRepository(db);
                }
                return cardRepository;
            }
        }

        public IPlayerRepository Players
        {
            get
            {
                if (playerRepository == null)
                {
                    playerRepository = new PlayerRepository(db);
                }
                return playerRepository;
            }
        }

        public IPlayerCardRepository PlayerCards
        {
            get
            {
                if (playerCardRepository == null)
                {
                    playerCardRepository = new PlayerCardRepository(db);
                }
                return playerCardRepository;
            }
        }


        public void Save ()
        {
            db.SaveChanges();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
