using System;
using System.Collections.Generic;
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
    public class GamePlayerRepository : IGamePlayerRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _connectionString;


        public GamePlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<GamePlayer>> GetByGameId(int gameId)
        {
            string sqlQuery = $@"SELECT * FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = {gameId}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayer> Get(int id)
        {
            string sqlQuery = $@"SELECT Id, PlayerId, GameId, Score, Bet, RoundScore, BetPayCoefficient 
                                 FROM GamePlayers 
                                 WHERE Id = {id}";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    GamePlayer gamePlayer = await db.QuerySingleAsync<GamePlayer>(sqlQuery);
                    return gamePlayer;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayer> Create(GamePlayer gamePlayer)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    int gamePlayerId = await db.InsertAsync(gamePlayer);
                    gamePlayer.Id = gamePlayerId;
                    return gamePlayer;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task Update(GamePlayer gamePlayer)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.UpdateAsync(gamePlayer);
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
                    await db.DeleteAsync(new GamePlayer() { Id = id } );
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task DeleteByGameId(int gameId)
        {
            string sqlQuery = $@"DELETE FROM GamePlayers 
                                 WHERE GameId = {gameId}";

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
