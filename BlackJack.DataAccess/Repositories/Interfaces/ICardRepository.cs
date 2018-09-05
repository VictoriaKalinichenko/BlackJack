using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAll();
    }
}
