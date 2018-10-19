using BlackJack.Entities.Entities;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerCardRepository : IBaseRepository<PlayerCard>
    {
        Task DeleteAllByGameId(long gameId);
    }
}
