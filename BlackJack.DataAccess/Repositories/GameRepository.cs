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
    public class GameRepository : IGameRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public async Task<Game> Get(int id)
        {
            string sqlQuery = $@"SELECT * FROM Games 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    Game game = await db.QuerySingleAsync<Game>(sqlQuery);
                    return game;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<Game> Create(Game game)
        {
            string sqlQuery = $@"INSERT INTO Games (Stage) 
                                 VALUES({game.Stage}); 
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    int gameId = await db.QuerySingleAsync<int>(sqlQuery);
                    game.Id = gameId;
                    return game;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task Update(Game game)
        {
            string sqlQuery = $@"UPDATE Games SET Stage = {game.Stage} 
                                 WHERE Id = {game.Id}";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    await db.QueryAsync(sqlQuery);
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
            string sqlQuery = $@"DELETE FROM Games 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
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
