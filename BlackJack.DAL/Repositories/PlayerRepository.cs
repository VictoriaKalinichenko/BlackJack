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
            player = db.Players.Where(m => m.Name.Equals(Name)).First();
            return player;
        }

        public void Create(Player obj)
        {
            db.Players.Add(obj);
        }

        public void Update(Player obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            Player player = db.Players.Find(Id);
            if (player != null)
                db.Players.Remove(player);
        }
    }
}
