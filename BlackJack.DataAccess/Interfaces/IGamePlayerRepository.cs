using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<IEnumerable<GamePlayer>> GetByGameId(int gameId);

        Task<GamePlayer> Get(int id);

        Task<int> GetGameIdByPlayerId(int id);

        Task<GamePlayer> Create(GamePlayer gamePlayer);

        Task Update(GamePlayer gamePlayer);

        Task Delete(int id);

        Task DeleteByGameId(int gameId);
    }
}
