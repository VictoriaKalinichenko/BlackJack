using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository : IBaseRepository<GamePlayer>
    {
        Task<IEnumerable<GamePlayer>> GetAllByGameId(long gameId);

        Task<GamePlayer> GetHumanByGameId(long gamePlayerId);
    }
}
