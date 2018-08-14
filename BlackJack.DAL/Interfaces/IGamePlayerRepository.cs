using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IGamePlayerRepository
    {
        IEnumerable<GamePlayer> GetAll();

        GamePlayer Get(int Id);

        void Create(GamePlayer obj);

        void Update(GamePlayer obj);

        void Delete(int Id);
    }
}
