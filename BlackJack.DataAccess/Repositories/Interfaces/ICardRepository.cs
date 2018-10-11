using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Task<IEnumerable<Card>> ResumeDeck(long gameId);
    }
}
