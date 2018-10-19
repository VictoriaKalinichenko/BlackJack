using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<long> GetIdByPlayerId(long id);

        Task<Game> GetByPlayerId(long playerId);

        Task UpdateStage(long gameId, GameStage stage);

        Task UpdateResult(long gameId, string result);
    }
}
