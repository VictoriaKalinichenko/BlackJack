using BlackJack.Entities.Entities;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerCardRepository : IGenericRepository<PlayerCard>
    {
        Task DeleteAllByGameId(long gameId);
    }
}
