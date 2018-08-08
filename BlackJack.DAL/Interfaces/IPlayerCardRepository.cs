using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IPlayerCardRepository
    {
        IEnumerable<PlayerCard> SelectAll();

        PlayerCard Select(int Id);

        void Create(PlayerCard obj);

        void Delete(int Id);
    }
}
