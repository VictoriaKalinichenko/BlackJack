using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<int> GetScoreById(long gamePlayerId);

        Task<GamePlayer> GetSpecificPlayerForStartRound(long gameId, int playerType);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersForStartRound(long gameId, int playerType);

        Task<GamePlayer> GetSpecificPlayerWithoutCards(long gameId, int playerType);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithoutCards(long gameId, int playerType);

        Task<GamePlayer> GetSpecificPlayerWithCards(long gamePlayerId, int playerCard);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithCards(long gameId, int playerType);
        
        Task CreateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateAfterAddingOneMoreCard(GamePlayer gamePlayer);

        Task UpdateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateManyAfterRoundSecondPhase(IEnumerable<GamePlayer> gamePlayers);

        Task DeleteBotsWithZeroScore(long gameId);

        Task DeleteAllByGameId(long gameId);
    }
}
