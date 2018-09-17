using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> SelectByName(string name);

        Task<Player> Get(int id);

        Task<Player> Create(Player obj);

        Task Delete(int id);
    }
}
