using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<List<Card>> GetSpecifiedAmount(int cardAmount);
    }
}
