using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IPlayerRepository : IDisposable
    {
        IEnumerable<Player> GetAll();

        Player Get(int Id);

        Player SelectByName(string Name);

        void Create(Player obj);

        void Update(Player obj);

        void Delete(int Id);
    }
}
