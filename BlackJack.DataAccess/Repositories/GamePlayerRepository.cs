using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                 {
                     gamePlayer.Player = player;
                     return gamePlayer;
                 });

                return gamePlayers;
            }
        }

        public async Task<GamePlayer> Get(int id)
        {
            string sqlQuery = $@"SELECT Id, PlayerId, GameId, Score, Bet, RoundScore, BetPayCoefficient 
                                 FROM GamePlayers 
                                 WHERE Id = {id}";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                GamePlayer gamePlayer = await db.QuerySingleOrDefaultAsync<GamePlayer>(sqlQuery);
                return gamePlayer;
            }
        }

        public async Task<int> GetGameIdByPlayerId(int id)
        {
            string sqlQuery = $@"SELECT TOP (1) GameId FROM GamePlayers 
                                 WHERE PlayerId = {id}
                                 ORDER BY ID DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gameId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery);
                return gameId;
            }
        }

        public async Task<GamePlayer> Create(GamePlayer gamePlayer)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gamePlayerId = await db.InsertAsync(gamePlayer);
                gamePlayer.Id = gamePlayerId;
                return gamePlayer;
            }
        }

        public async Task Update(GamePlayer gamePlayer)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync(gamePlayer);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(new GamePlayer() { Id = id });
            }
        }

        public async Task DeleteByGameId(int gameId)
        {
            string sqlQuery = $@"DELETE FROM GamePlayers 
                                 WHERE GameId = {gameId}";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sqlQuery);
            }
        }
    }
}
