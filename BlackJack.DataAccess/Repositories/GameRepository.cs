using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(string connectionString) : base(connectionString)
        { }
        
        public async Task<long> GetIdByPlayerId(long playerId)
        {
            string sqlQuery = $@"SELECT TOP (1) GameId FROM GamePlayers 
                                 WHERE PlayerId = @playerId
                                 ORDER BY CreationDate DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                long gameId = await db.QueryFirstOrDefaultAsync<long>(sqlQuery, new { playerId });
                return gameId;
            }
        }

        public async Task<Game> GetByPlayerId(long playerId)
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

        public async Task UpdateRoundResult(long id, string roundResult)
        {
            string sqlQuery = @"UPDATE Games SET RoundResult = @roundResult
                                WHERE Id = @id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery,
                    new { roundResult = roundResult, id = id });
            }
        }
    }
}
