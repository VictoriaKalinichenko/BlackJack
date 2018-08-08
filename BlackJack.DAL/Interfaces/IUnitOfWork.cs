using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Cards { get; }

        IPlayerRepository Players { get; }

        IPlayerCardRepository PlayerCards { get; }

        void Save();
    }
}
