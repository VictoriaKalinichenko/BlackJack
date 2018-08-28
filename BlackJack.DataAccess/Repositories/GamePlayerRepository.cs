using System;
using System.Collections.Generic;
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
    public class GamePlayerRepository : IGamePlayerRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public async Task<IEnumerable<GamePlayer>> GetByGameId(int gameId)
        {
            string sqlQuery = $@"SELECT * FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = {gameId}";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    var gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery,(gamePlayer, player) =>
                    {
                        gamePlayer.Player = player;
                        return gamePlayer;
                    });

                    return gamePlayers;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayer> Get(int id)
        {
            string sqlQuery = $@"SELECT * FROM GamePlayers 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    GamePlayer gamePlayer = await db.QuerySingleAsync<GamePlayer>(sqlQuery);
                    return gamePlayer;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<GamePlayer> Create(GamePlayer gamePlayer)
        {
            string sqlQuery = $@"INSERT INTO GamePlayers (PlayerId, GameId, Score, BetPayCoefficient, Bet, RoundScore) 
                                 VALUES(@PlayerId, @GameId, @Score, @BetPayCoefficient, @Bet, @RoundScore); 
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
                {
                    int gamePlayerId = await db.QuerySingleAsync<int>(sqlQuery, gamePlayer);
                    gamePlayer.Id = gamePlayerId;
                    return gamePlayer;
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task Update(GamePlayer gamePlayer)
        {
            string sqlQuery = $@"UPDATE GamePlayers 
                                 SET Score = {gamePlayer.Score}, Bet = {gamePlayer.Bet}, BetPayCoefficient = {gamePlayer.BetPayCoefficient}, RoundScore = {gamePlayer.RoundScore} 
                                 WHERE Id = {gamePlayer.Id}";

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
            string sqlQuery = $@"DELETE FROM GamePlayers 
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

        public async Task DeleteByGameId(int gameId)
        {
            string sqlQuery = $@"DELETE FROM GamePlayers 
                                 WHERE GameId = {gameId}";

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
