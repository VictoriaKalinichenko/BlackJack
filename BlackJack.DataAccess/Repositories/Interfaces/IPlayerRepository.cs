using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<List<Player>> CreateMany(List<Player> players);
    }
}
