using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IHistoryMessageManager
    {
        Task AddCreationGameMessages(List<GamePlayer> gamePlayers, Game game);

        Task AddStartRoundMessages(List<GamePlayer> gamePlayers, long gameId);

        Task AddContinueRoundMessages(List<GamePlayer> gamePlayers, List<PlayerCard> playerCards, long gameId);
    }
}
