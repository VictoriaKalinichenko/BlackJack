using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper.Contrib.Extensions;
using Z.BulkOperations;

namespace BlackJack.DataAccess.Repositories
{
    public class LogRepository : ILogRepository
    {
        private string _connectionString;


        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "Logs";
                await bulkOperation.BulkInsertAsync(logs);
                db.Close();
            }
        }

        public async Task Create(int gameId, string message, DateTime dateTime)
        {
            var log = new Log() { DateTime = dateTime, GameId = gameId, Message = message };
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(log);
            }
        }
    }
}
