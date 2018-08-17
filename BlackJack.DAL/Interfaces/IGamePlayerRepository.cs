using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;

namespace BlackJack.DAL.Interfaces
{
    public interface IGamePlayerRepository
    {
        IEnumerable<GamePlayer> GetAll();

        GamePlayer Get(int id);

        void Create(GamePlayer obj);

        void Update(GamePlayer obj);

        void Delete(int id);

        GamePlayer GetLastObj();
    }
}
