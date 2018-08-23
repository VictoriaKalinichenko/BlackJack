using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IGameRepository
    {
        List<Game> GetAll();

        Game Get(int id);

        Game Create(Game obj);

        void Update(Game obj);

        void Delete(int id);
    }
}
