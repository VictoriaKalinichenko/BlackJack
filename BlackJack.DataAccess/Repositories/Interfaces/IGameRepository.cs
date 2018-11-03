using BlackJack.Entities.Entities;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<Game> GetByHumanName(string playerName);
    }
}
