using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
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
                                 WHERE A.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                     gamePlayer.Player = player;
                     return gamePlayer;
                },
                new { gameId },
                null);

                return gamePlayers;
            }
        }

        public async Task<GamePlayer> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                GamePlayer gamePlayer = await db.GetAsync<GamePlayer>(id);
                return gamePlayer;
            }
        }

        public async Task<int> GetGameIdByPlayerId(int id)
        {
            string sqlQuery = $@"SELECT TOP (1) GameId FROM GamePlayers 
                                 WHERE PlayerId = @id
                                 ORDER BY CreationDate DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gameId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, new { id });
                return gameId;
            }
        }

        public async Task<GamePlayer> GetSpecificPlayerByGameId(int gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, PlayerId, GameId, Score, RoundScore, BetPayCoefficient, Bet, A.CreationDate
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId AND B.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                GamePlayer player = await db.QueryFirstOrDefaultAsync<GamePlayer>(sqlQuery, new { gameId = gameId, playerType = playerType });
                return player;
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
                                 WHERE GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { gameId });
            }
        }
    }
}
