using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : BaseRepository<PlayerCard>, IPlayerCardRepository 
    {
        public PlayerCardRepository(string connectionString) : base(connectionString)
        { }
        
        public async Task DeleteAllByGameId(long gameId)
        {
            string sqlQuery = $@"DELETE FROM PlayerCards
                                 WHERE Id IN (SELECT A.Id FROM PlayerCards AS A
                                 INNER JOIN GamePlayers AS B ON A.GamePlayerId = B.Id
                                 WHERE B.GameId = @gameId)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync<PlayerCard>(sqlQuery, new { gameId });
            }
        }
    }
}
