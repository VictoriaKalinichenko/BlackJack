using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAll();

        Task Create(int gameId, string message);
    }
}
