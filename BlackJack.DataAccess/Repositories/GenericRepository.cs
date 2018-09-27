using BlackJack.DataAccess.Repositories.Interfaces;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> Get(long id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                T item = await db.GetAsync<T>(id);
                return item;
            }
        }

        public async Task<long> Create(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                long id = await db.InsertAsync(item);
                return id;
            }
        }

        public async Task Delete(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(item);
            }
        }
    }
}
