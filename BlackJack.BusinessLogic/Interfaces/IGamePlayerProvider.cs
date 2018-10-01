using BlackJack.Entities.Entities;
using System.Collections.Generic;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerProvider
    {
        void CreateBets(List<GamePlayer> players, int bet);

        void DefinePayCoefficientsAfterRoundStart(List<GamePlayer> players);

        void DefinePayCoefficientsAfterRoundContinue(List<GamePlayer> players);

        void PayBets(List<GamePlayer> players);

        bool IsEnoughCardsForHuman(int roundScore);

        bool IsEnoughCardsForBot(int roundScore);

        string GetHumanRoundResult(float betPayCoefficient);
    }
}
