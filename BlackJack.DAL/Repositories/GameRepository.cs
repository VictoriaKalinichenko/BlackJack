using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.Entity.Models;
using System.Data.Entity;

namespace BlackJack.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        public IEnumerable<Game> GetAll()
        {
            IEnumerable<Game> games;

            using (DataBaseContext db = new DataBaseContext())
            {
                games = db.Games;
            }

            return games;
        }

        public Game Get(int id)
        {
            Game Game;

            using (DataBaseContext db = new DataBaseContext())
            {
                Game = db.Games.Find(id);
            }

            return Game;
        }

        public void Create(Game obj)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Games.Add(obj);
                db.SaveChanges();
            }
        }

        public void Update(Game obj)
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
                Game Game = db.Games.Find(id);
                if (Game != null)
                {
                    db.Games.Remove(Game);
                }
                db.SaveChanges();
            }
        }

        public Game GetLastObj()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Game game;

                game = db.Games.Find(db.Games.Count());

                return game;
            }
        }
    }
}
