using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;
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
            string sqlQuery = $@"SELECT * FROM PlayerCards 
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
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<PlayerCard> Get(int id)
        {
            string sqlQuery = $@"SELECT * FROM PlayerCards 
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
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<PlayerCard> Create(PlayerCard playerCard)
        {
            string sqlQuery = $@"INSERT INTO PlayerCards (GamePlayerId, CardId) 
                                 VALUES({playerCard.GamePlayerId}, {playerCard.CardId}); 
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int playerCardId = await db.QuerySingleAsync<int>(sqlQuery);
                    playerCard.Id = playerCardId;
                    return playerCard;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
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
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }
    }
}
