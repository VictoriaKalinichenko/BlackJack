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
    public class PlayerRepository : IPlayerRepository
    {
        private DataBaseContext db;

        public PlayerRepository(DataBaseContext context)
        {
            db = context;
        }



        public IEnumerable<Player> GetAll()
        {
            IEnumerable<Player> players;
            players = db.Players;
            return players;
        }

        public Player Get(int Id)
        {
            Player player;
            player = db.Players.Find(Id);
            return player;
        }

        public Player SelectByName(string Name)
        {
            Player player;
            player = db.Players.Where(m => m.Name.Equals(Name)).FirstOrDefault();
            return player;
        }

        public void Create(Player obj)
        {
            db.Players.Add(obj);
            db.SaveChanges();
        }

        public void Update(Player obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            Player player = db.Players.Find(Id);
            if (player != null)
                db.Players.Remove(player);
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
