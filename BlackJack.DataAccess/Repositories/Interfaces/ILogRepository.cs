using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAll();

        Task CreateMany(IEnumerable<Log> logs);
    }
}
