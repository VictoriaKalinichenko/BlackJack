using BlackJack.Entities.Entities;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<long> GetIdByPlayerId(long id);

        Task<Game> GetByPlayerId(long playerId);

        Task<Game> Create();

        Task UpdateStage(long gameId, int stage);

        Task UpdateResult(long gameId, string result);
    }
}
