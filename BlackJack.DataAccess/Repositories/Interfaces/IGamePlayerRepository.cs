using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<int> GetScoreById(long gamePlayerId);

        Task<IEnumerable<GamePlayer>> GetAllForInitRound(long gameId);

        Task<IEnumerable<GamePlayer>> GetAllWithoutCards(long gameId);

        Task<GamePlayer> GetWithCards(long gamePlayerId, int playerCard);

        Task<IEnumerable<GamePlayer>> GetAllWithCards(long gameId);
        
        Task CreateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateAddingCard(GamePlayer gamePlayer);

        Task UpdateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateManyAfterContinueRound(IEnumerable<GamePlayer> gamePlayers);

        Task DeleteBotsWithZeroScore(long gameId);

        Task DeleteAllByGameId(long gameId);
    }
}
