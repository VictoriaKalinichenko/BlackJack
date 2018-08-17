using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.Entity.Models;

namespace BlackJack.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public IEnumerable<Player> GetAll()
        {
            IEnumerable<Player> players;

            using (DataBaseContext db = new DataBaseContext())
            {
                players = db.Players;
            }

            return players;
        }

        public Player Get(int id)
        {
            Player player;

            using (DataBaseContext db = new DataBaseContext())
            {
                player = db.Players.Find(id);
            }

            return player;
        }

        public Player SelectByName(string name)
        {
            Player player;

            using (DataBaseContext db = new DataBaseContext())
            {
                player = db.Players.Where(m => m.Name.Equals(name)).FirstOrDefault();
            }

            return player;
        }

        public void Create(Player obj)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Players.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Player player = db.Players.Find(id);
                if (player != null)
                {
                    db.Players.Remove(player);
                }
                db.SaveChanges();
            }
        }

        public Player GetLastObj()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Player player;

                player = db.Players.Find(db.Players.Count());

                return player;
            }
        }

        public Player GetDealer()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Player dealer = null;

                IEnumerable<Player> players = db.Players;

                foreach(Player player in players)
                {
                    if (player.IsDealer)
                    {
                        dealer = player;
                        break;
                    }
                }

                return dealer;
            }
        }

        public List<Player> GetBots(int amount)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                List<Player> bots = new List<Player>();

                List<Player> players = db.Players.ToList();

                int count = 0;
                foreach (Player player in players)
                {
                    if (!player.IsDealer && !player.IsHuman)
                    {
                        bots.Add(player);
                        count++;
                    }

                    if (count >= amount)
                    {
                        break;
                    }
                }

                return bots;
            }
        }
    }
}
