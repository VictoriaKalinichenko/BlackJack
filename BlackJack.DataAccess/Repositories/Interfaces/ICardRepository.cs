using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAll();
    }
}
