using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAll();

        Player SelectByName(string name);

        Player Get(int id);

        Player Create(Player obj);

        void Delete(int id);
    }
}
