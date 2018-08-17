using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.Entity.Models;
using BlackJack.DAL.Context;
using System.Data.Entity;

namespace BlackJack.DAL.Repositories
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
        public IEnumerable<GamePlayer> GetAll()
        {
            IEnumerable<GamePlayer> gamePlayers;

            using (DataBaseContext db = new DataBaseContext())
            {
                gamePlayers = db.GamePlayers;
            }

            return gamePlayers;
        }

        public GamePlayer Get(int id)
        {
            GamePlayer gamePlayer;

            using (DataBaseContext db = new DataBaseContext())
            {
                gamePlayer = db.GamePlayers.Find(id);
            }

            return gamePlayer;
        }

        public void Create(GamePlayer obj)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.GamePlayers.Add(obj);
                db.SaveChanges();
            }
        }

        public void Update(GamePlayer obj)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                GamePlayer gamePlayer = db.GamePlayers.Find(id);
                if (gamePlayer != null)
                {
                    db.GamePlayers.Remove(gamePlayer);
                }
                db.SaveChanges();
            }
        }


        public GamePlayer GetLastObj()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                GamePlayer gamePlayer;

                gamePlayer = db.GamePlayers.Find(db.GamePlayers.Count());

                return gamePlayer;
            }
        }
    }
}
