using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private string _connectionString;


        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Game> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Game game = await db.GetAsync<Game>(id);
                return game;
            }
        }
        
        public async Task<int> GetIdByPlayerId(int playerId)
        {
            string sqlQuery = $@"SELECT TOP (1) GameId FROM GamePlayers 
                                 WHERE PlayerId = @playerId
                                 ORDER BY CreationDate DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gameId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, new { playerId });
                return gameId;
            }
        }

        public async Task<Game> GetByPlayerId(int playerId)
        {
            string sqlQuery = $@"SELECT TOP (1) B.Id, B.Stage, B.Result 
                                 FROM GamePlayers AS A
                                 INNER JOIN Games AS B ON A.GameId = B.Id
                                 WHERE A.PlayerId = @playerId
                                 ORDER BY A.CreationDate DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Game game = await db.QueryFirstOrDefaultAsync<Game>(sqlQuery, new { playerId = playerId });
                return game;
            }
        }

        public async Task<Game> Create(Game game)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gameId = await db.InsertAsync(game);
                game.Id = gameId;
                return game;
            }
        }

        public async Task UpdateStage(int gameId, int stage)
        {
            string sqlQuery = @"UPDATE Games SET Stage = @stage
                                WHERE Id = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { stage = stage, gameId = gameId });
            }
        }

        public async Task UpdateResult(int gameId, string result)
        {
            string sqlQuery = @"UPDATE Games SET Result = @result
                                WHERE Id = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { result = result, gameId = gameId });
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(new Game() { Id = id });
            }
        }
    }
}
