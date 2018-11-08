using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository : IBaseRepository<GamePlayer>
    {
        Task<List<GamePlayer>> GetAllByGameId(long gameId);
    }
}
