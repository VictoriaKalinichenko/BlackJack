using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using Dapper;
using Dapper.Contrib.Extensions;
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
            string sqlQuery = @"SELECT Id, Players.Name, IsHuman, IsDealer FROM Players    
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<Player> Get(int id)
        {
            Player player = new Player();
            string sqlQuery = $@"SELECT Id, Players.Name, IsHuman, IsDealer FROM Players 
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<Player> Create(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int playerId = await db.InsertAsync(player);
                    player.Id = playerId;
                    return player;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.DeleteAsync(new Player() { Id = id });
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
