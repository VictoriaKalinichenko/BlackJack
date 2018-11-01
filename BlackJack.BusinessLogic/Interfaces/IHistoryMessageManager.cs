using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IHistoryMessageManager
    {
        Task AddMessagesForCreateGame(List<GamePlayer> gamePlayers, Game game);

        Task AddMessagesForStartRound(List<GamePlayer> gamePlayers, long gameId);

        Task AddMessagesForContinueRound(List<GamePlayer> gamePlayers, List<PlayerCard> playerCards, long gameId);
    }
}
