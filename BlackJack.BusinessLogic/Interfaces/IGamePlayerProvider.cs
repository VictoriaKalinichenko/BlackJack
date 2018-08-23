using System.Collections.Generic;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerProvider
    {
        List<GamePlayer> BetsCreation(List<GamePlayer> players, int bet);

        void RoundBetPayments(List<GamePlayer> players, int oneToOnePayKey = 0);
    }
}
