using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private string _connectionString;


        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Player> SelectByName(string name)
        {
            Player player = new Player();
            string sqlQuery = @"SELECT Id, Players.Name, IsHuman, IsDealer FROM Players    
                                WHERE Players.Name = @name";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                player = await db.QueryFirstOrDefaultAsync<Player>(sqlQuery, new { name });
                return player;
            }
        }

        public async Task<Player> Get(int id)
        {
            Player player = new Player();
            string sqlQuery = $@"SELECT Id, Players.Name, IsHuman, IsDealer FROM Players 
                                 WHERE Id = {id}";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                player = await db.QuerySingleOrDefaultAsync<Player>(sqlQuery);
                return player;
            }
        }

        public async Task<Player> Create(Player player)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int playerId = await db.InsertAsync(player);
                player.Id = playerId;
                return player;
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(new Player() { Id = id });
            }
        }
    }
}
