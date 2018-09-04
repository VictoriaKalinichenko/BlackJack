using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IPlayerCardRepository
    {
        Task<IEnumerable<PlayerCard>> GetByGamePlayerId(int gamePlayerId);

        Task<int> GetCountByGamePlayerId(int gamePlayerId);

        Task<PlayerCard> Get(int id);

        Task<PlayerCard> Create(PlayerCard obj);

        Task DeleteByGamePlayerId(int gamePlayerId);
    }
}
