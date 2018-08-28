using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IPlayerCardRepository
    {
        Task<IEnumerable<PlayerCard>> GetByGamePlayerId(int gamePlayerId);

        Task<PlayerCard> Get(int id);

        Task<PlayerCard> Create(PlayerCard obj);

        Task DeleteByGamePlayerId(int gamePlayerId);
    }
}
