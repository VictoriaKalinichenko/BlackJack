using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<Player> SelectByName(string name, int playerType);

        Task<List<Player>> CreateMany(List<Player> players);
    }
}
