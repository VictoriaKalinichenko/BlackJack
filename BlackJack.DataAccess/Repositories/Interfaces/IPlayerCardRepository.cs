using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerCardRepository
    {
        Task<IEnumerable<PlayerCard>> GetByGamePlayerId(int gamePlayerId);
        
        Task<IEnumerable<int>> GetCardsOnHandsIdsByGameId(int gameId);

        Task<IEnumerable<PlayerCard>> GetPlayerCardsByGameId(int gameId);
        
        Task<PlayerCard> Create(PlayerCard obj);

        Task CreateMany(IEnumerable<PlayerCard> playerCards);

        Task DeleteMany(IEnumerable<PlayerCard> playerCards);
    }
}
