using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Context;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public List<Player> GetAll()
        {
            List<Player> players = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                players = db.Query<Player>("SELECT * FROM Players").ToList();
            }
            return players;
        }

        public Player SelectByName(string name)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                IEnumerable<Player> players = db.Players;
            }

            Player player = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                player = db.Query<Player>("SELECT * FROM Players WHERE Players.Name = @name", new { name }).FirstOrDefault();
            }
            return player;
        }

        public Player Get(int id)
        {
            Player player = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                player = db.Query<Player>("SELECT * FROM Players WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return player;
        }

        public Player Create(Player player)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "INSERT INTO Players (Name, IsHuman, IsDealer) VALUES(@Name, @IsHuman, @IsDealer); SELECT CAST(SCOPE_IDENTITY() as int)";
                int playerId = db.Query<int>(sqlQuery, player).FirstOrDefault();
                player.Id = playerId;
            }
            return player;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "DELETE FROM Players WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
