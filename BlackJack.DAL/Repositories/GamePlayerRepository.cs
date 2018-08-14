using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.Entity;
using BlackJack.DAL.Context;
using System.Data.Entity;

namespace BlackJack.DAL.Repositories
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
        private DataBaseContext db;

        public GamePlayerRepository(DataBaseContext context)
        {
            db = context;
        }


        public IEnumerable<GamePlayer> GetAll()
        {
            IEnumerable<GamePlayer> gamePlayers;
            gamePlayers = db.GamePlayers;
            return gamePlayers;
        }

        public GamePlayer Get(int Id)
        {
            GamePlayer gamePlayer;
            gamePlayer = db.GamePlayers.Find(Id);
            return gamePlayer;
        }

        public void Create(GamePlayer obj)
        {
            db.GamePlayers.Add(obj);
            db.SaveChanges();
        }

        public void Update(GamePlayer obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            GamePlayer gamePlayer = db.GamePlayers.Find(Id);
            if (gamePlayer != null)
                db.GamePlayers.Remove(gamePlayer);
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
