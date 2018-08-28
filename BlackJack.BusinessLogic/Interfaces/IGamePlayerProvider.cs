using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerProvider
    {
        Task BetsCreation(IEnumerable<GamePlayer> players, int bet);

        Task RoundBetPayments(IEnumerable<GamePlayer> players, int oneToOnePayKey = 0);
    }
}
