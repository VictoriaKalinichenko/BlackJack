using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;

namespace BlackJack.DAL.Interfaces
{
    public interface IPlayerCardRepository
    {
        IEnumerable<PlayerCard> GetAll();

        PlayerCard Get(int id);

        void Create(PlayerCard obj);

        void DeleteByGamePlayerId(int gamePlayerId);
    }
}
