using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace BlackJack.DataAccess.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<Log>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Log> logs = await db.GetAllAsync<Log>();
                return logs;
            }
        }

        public async Task CreateMany(IEnumerable<Log> logs)
        {
            DbConnection db = new SqlConnection(_connectionString);
            db.Open();
            var bulkOperation = new BulkOperation(db);
            bulkOperation.DestinationTableName = "Logs";
            await bulkOperation.BulkInsertAsync(logs);
            db.Close();
        }
    }
}
