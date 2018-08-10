using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface ICardRepository : IDisposable
    {
        IEnumerable<Card> GetAll();

        Card Get(int Id);
    }
}
