using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository : IBaseRepository<GamePlayer>
    {
        Task<int> GetScoreById(long gamePlayerId);

        Task<IEnumerable<GamePlayer>> GetAllForInitRound(long gameId);

        Task<IEnumerable<GamePlayer>> GetAllWithoutCards(long gameId);

        Task<GamePlayer> GetWithCards(long gamePlayerId, int playerCard);

        Task<IEnumerable<GamePlayer>> GetAllWithCards(long gameId);
        
        Task UpdateAddingCard(GamePlayer gamePlayer);

        Task UpdateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateManyAfterContinueRound(IEnumerable<GamePlayer> gamePlayers);

        Task DeleteBotsWithZeroScore(long gameId);

        Task DeleteAllByGameId(long gameId);
    }
}
