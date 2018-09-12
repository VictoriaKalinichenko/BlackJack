using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
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

        public async Task<Game> Create(Game game)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int gameId = await db.InsertAsync(new Game() { Stage = game.Stage });
                game.Id = gameId;
                return game;
            }
        }

        public async Task Update(Game game)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync(game);
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
