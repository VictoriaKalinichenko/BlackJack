using BlackJack.Entities.Entities;
using System.Collections.Generic;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerManager
    {
        void CreateBets(List<GamePlayer> players, int bet);

        void DefinePayCoefficientsAfterRoundStart(List<GamePlayer> players);

        void DefinePayCoefficientsAfterRoundContinue(List<GamePlayer> players);

        void PayBets(List<GamePlayer> players);

        string GetHumanRoundResult(float betPayCoefficient);
    }
}
