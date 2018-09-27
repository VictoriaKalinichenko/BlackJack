using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerProvider
    {
        Task CreateBets(IEnumerable<GamePlayer> bots, GamePlayer human, int bet, List<Log> logs);

        void PayBets(IEnumerable<GamePlayer> players, GamePlayer dealer);

        void DefinePayCoefficientsAfterRoundStart(IEnumerable<GamePlayer> players, GamePlayer dealer, List<Log> logs);

        void DefinePayCoefficientsAfterRoundContinue(IEnumerable<GamePlayer> players, GamePlayer dealer, List<Log> logs);

        bool IsEnoughCardsForHuman(int roundScore);

        bool IsEnoughCardsForBot(int roundScore);

        string GetHumanRoundResult(float betPayCoefficient);
    }
}
