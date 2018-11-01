using BlackJack.Entities.Entities;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<long> GetIdByPlayerId(long id);

        Task<Game> GetByPlayerId(long playerId);

        Task<string> GetHumanNameByGameId(long gameId);

        Task UpdateRoundResult(long id, string roundResult);
    }
}
