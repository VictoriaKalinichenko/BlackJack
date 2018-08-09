using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.DAL.Interfaces
{
    public interface IDeckRepository
    {
        Deck Get(int Id);

        void Create(Deck obj);

        void Update(Deck obj);

        void Delete(int Id);
    }
}
