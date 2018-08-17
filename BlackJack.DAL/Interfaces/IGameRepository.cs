using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;

namespace BlackJack.DAL.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();

        Game Get(int id);

        void Create(Game obj);

        void Update(Game obj);

        void Delete(int id);

        Game GetLastObj();
    }
}
