using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IHistoryMessageManager
    {
        Task AddMessagesForCreateGame(List<GamePlayer> gamePlayers, Game game);

        Task AddMessagesForRound(List<GamePlayer> gamePlayers, string roundResult, long gameId);
    }
}
