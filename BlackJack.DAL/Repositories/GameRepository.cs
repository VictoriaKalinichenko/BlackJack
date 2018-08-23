using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;

namespace BlackJack.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        public List<Game> GetAll()
        {
            List<Game> games = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                games = db.Query<Game>("SELECT * FROM Games").ToList();
            }
            return games;
        }

        public Game Get(int id)
        {
            Game game = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                game = db.Query<Game>("SELECT * FROM Games WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return game;
        }

        public Game Create(Game game)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "INSERT INTO Games (Stage) VALUES(@Stage); SELECT CAST(SCOPE_IDENTITY() as int)";
                int gameId = db.Query<int>(sqlQuery, game).FirstOrDefault();
                game.Id = gameId;
            }
            return game;
        }

        public void Update(Game game)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "UPDATE Games SET Stage = @Stage WHERE Id = @Id";
                db.Execute(sqlQuery, game);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "DELETE FROM Games WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
