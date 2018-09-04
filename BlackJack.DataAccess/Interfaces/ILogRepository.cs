using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAll();

        Task CreateLogGameIsCreated(int gameId, int stage);

        Task CreateLogPlayerIsAddedToGame(int gameId, Player player, int score);

        Task CreateLogRoundIsStarted(int gameId);

        Task CreateLogBetIsCreated(int gameId, Player player, int score, int bet);

        Task CreateLogCardIsAdded(int gameId, Player player, int roundScore, int cardId, int cardValue, string cardName);

        Task CreateLogDealerBjDanger(int gameId, Player player, int cardId, int cardValue, string cardName);

        Task CreateLogPlayerBj(int gameId, Player player, int roundScore, float betPayCoef);

        Task CreateLogPlayerBjAndDealerBjDanger(int gameId, Player player, int roundScore, float betPayCoef);

        Task CreateLogGameStageIsChanged(int gameId, int stage);
    }
}
