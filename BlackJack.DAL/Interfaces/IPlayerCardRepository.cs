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
        List<PlayerCard> GetByGamePlayerId(int gamePlayerId);

        PlayerCard Get(int id);

        PlayerCard Create(PlayerCard obj);

        void DeleteByGamePlayerId(int gamePlayerId);
    }
}
