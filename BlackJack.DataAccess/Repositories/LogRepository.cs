using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper.Contrib.Extensions;

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
                var logs = await db.GetAllAsync<Log>();
                return logs;
            }
        }

        public async Task Create(int gameId, string message)
        {
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(log);
            }
        }
    }
}
