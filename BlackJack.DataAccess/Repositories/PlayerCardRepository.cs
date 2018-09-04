using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using NLog;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository 
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _connectionString;


        public PlayerCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlayerCard>> GetByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"SELECT Id, GamePlayerId, CardId FROM PlayerCards 
                                 WHERE GamePlayerId = {gamePlayerId}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var playerCards = await db.QueryAsync<PlayerCard>(sqlQuery);
                    return playerCards;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<int> GetCountByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"SELECT COUNT(GamePlayerId) FROM PlayerCards 
                                 WHERE GamePlayerId = {gamePlayerId}
                                 GROUP BY GamePlayerId";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int playerCardsCount = await db.QuerySingleAsync<int>(sqlQuery);
                    return playerCardsCount;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<PlayerCard> Get(int id)
        {
            string sqlQuery = $@"SELECT Id, GamePlayerId, CardId FROM PlayerCards 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    PlayerCard playerCard = await db.QuerySingleAsync<PlayerCard>(sqlQuery);
                    return playerCard;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<PlayerCard> Create(PlayerCard playerCard)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int playerCardId = await db.InsertAsync(playerCard);
                    playerCard.Id = playerCardId;
                    return playerCard;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task DeleteByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"DELETE FROM PlayerCards 
                                 WHERE GamePlayerId = {gamePlayerId}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.ExecuteAsync(sqlQuery);
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
