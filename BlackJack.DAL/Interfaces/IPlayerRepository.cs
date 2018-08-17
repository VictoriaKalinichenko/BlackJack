using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;

namespace BlackJack.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();

        Player Get(int id);

        Player SelectByName(string name);

        void Create(Player obj);

        void Delete(int id);

        Player GetLastObj();

        Player GetDealer();

        List<Player> GetBots(int amount);
    }
}
