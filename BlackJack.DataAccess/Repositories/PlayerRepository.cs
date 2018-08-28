using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;
using NLog;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _connectionString;


        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Player> SelectByName(string name)
        {
            Player player = new Player();
            string sqlQuery = @"SELECT * FROM Players    
                                WHERE Players.Name = @name";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    player = await db.QueryFirstOrDefaultAsync<Player>(sqlQuery, new { name });
                    return player;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<Player> Get(int id)
        {
            Player player = new Player();
            string sqlQuery = $@"SELECT * FROM Players 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    player = await db.QuerySingleAsync<Player>(sqlQuery);
                    return player;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<Player> Create(Player player)
        {
            string sqlQuery = @"INSERT INTO Players (Name, IsHuman, IsDealer) 
                                VALUES(@Name, @IsHuman, @IsDealer); 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int playerId = await db.QuerySingleAsync<int>(sqlQuery, player);
                    player.Id = playerId;
                    return player;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            string sqlQuery = $@"DELETE FROM Players 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.ExecuteAsync(sqlQuery);
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }
    }
}
