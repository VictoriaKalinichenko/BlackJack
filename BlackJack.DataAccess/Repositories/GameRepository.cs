using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
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
        
        public async Task<Game> GetByHumanName(string playerName)
        {
            string sqlQuery = $@"SELECT TOP (1) C.Id FROM GamePlayers AS A
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 INNER JOIN Games AS C ON A.GameId = C.Id
                                 WHERE B.Name = @playerName AND B.Type = @playerType
                                 ORDER BY C.CreationDate DESC";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Game game = await db.QueryFirstOrDefaultAsync<Game>(sqlQuery, 
                    new { playerName = playerName, playerType = PlayerType.Human });
                return game;
            }
        }
    }
}
