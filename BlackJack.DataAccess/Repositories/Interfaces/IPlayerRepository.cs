using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<Player> SelectByName(string name, int playerType);
        
        Task<Player> Create(Player obj);

        Task<List<Player>> CreateMany(List<Player> players);

        Task Delete(long id);
    }
}
