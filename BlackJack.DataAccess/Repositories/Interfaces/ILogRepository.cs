using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAll();

        Task Create(Log log);

        Task CreateMany(IEnumerable<Log> logs);
    }
}
