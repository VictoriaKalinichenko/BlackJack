using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> Get(int id);

        Task<int> GetIdByPlayerId(int id);

        Task<Game> GetByPlayerId(int playerId);

        Task<Game> Create();

        Task UpdateStage(int gameId, int stage);

        Task UpdateResult(int gameId, string result);

        Task Delete(int id);
    }
}
