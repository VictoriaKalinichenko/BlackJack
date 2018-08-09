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
    public class GameRepository : IGameRepository
    {
        private DataBaseContext db;

        public GameRepository(DataBaseContext context)
        {
            db = context;
        }



        public Game Get(int Id)
        {
            Game Game;
            Game = db.Games.Find(Id);
            return Game;
        }

        public void Create(Game obj)
        {
            db.Games.Add(obj);
        }

        public void Update(Game obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            Game Game = db.Games.Find(Id);
            if (Game != null)
                db.Games.Remove(Game);
        }
    }
}
