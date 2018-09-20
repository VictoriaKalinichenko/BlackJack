using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<int> GetScoreById(int gamePlayerId);

        Task<GamePlayer> GetSpecificPlayerForStartRound(int gameId, int playerType);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersForStartRound(int gameId, int playerType);

        Task<GamePlayer> GetSpecificPlayerWithoutCards(int gameId, int playerType);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithoutCards(int gameId, int playerType);

        Task<GamePlayer> GetSpecificPlayerWithCards(int gamePlayerId, int playerCard);

        Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithCards(int gameId, int playerType);
        
        Task CreateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateAfterAddingOneMoreCard(GamePlayer gamePlayer);

        Task UpdateMany(IEnumerable<GamePlayer> gamePlayers);

        Task UpdateManyAfterRoundSecondPhase(IEnumerable<GamePlayer> gamePlayers);

        Task DeleteBotsWithZeroScore(int gameId);

        Task DeleteAllByGameId(int gameId);
    }
}
