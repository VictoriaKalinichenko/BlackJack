using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(string connectionString) : base(connectionString)
        { }
    }
}
