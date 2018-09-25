using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> Get(long id);

        Task<long> GetIdByPlayerId(long id);

        Task<Game> GetByPlayerId(long playerId);

        Task<Game> Create();

        Task UpdateStage(long gameId, int stage);

        Task UpdateResult(long gameId, string result);

        Task Delete(long id);
    }
}
