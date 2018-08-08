using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> SelectAll();

        Player Select(int Id);

        Player SelectByName(string Name);

        void Create(Player obj);

        void Update(Player obj);

        void Delete(int Id);
    }
}
