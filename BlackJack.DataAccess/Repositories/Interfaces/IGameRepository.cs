using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> Get(int id);

        Task<Game> Create(Game obj);

        Task Update(Game obj);

        Task Delete(int id);
    }
}
