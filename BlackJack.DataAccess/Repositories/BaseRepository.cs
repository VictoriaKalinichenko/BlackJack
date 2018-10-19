using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected string _connectionString;

        public BaseRepository(string connectionString)
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

        public async Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<T> items = await db.GetAllAsync<T>();
                return items;
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

        public async Task CreateMany(IEnumerable<T> items)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(items);
            }
        }

        public async Task Delete(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(item);
            }
        }

        public async Task DeleteMany(IEnumerable<T> items)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(items);
            }
        }
    }
}
