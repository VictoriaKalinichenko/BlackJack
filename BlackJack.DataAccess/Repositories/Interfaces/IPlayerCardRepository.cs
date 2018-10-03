using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerCardRepository : IGenericRepository<PlayerCard>
    {
        Task<IEnumerable<PlayerCard>> GetByGamePlayerId(long gamePlayerId);
        
        Task<IEnumerable<Card>> GetCardsOnHands(long gameId);

        Task<IEnumerable<PlayerCard>> GetAllByGameId(long gameId);
        
        Task Create(PlayerCard obj);

        Task CreateMany(IEnumerable<PlayerCard> playerCards);

        Task DeleteMany(IEnumerable<PlayerCard> playerCards);
    }
}
