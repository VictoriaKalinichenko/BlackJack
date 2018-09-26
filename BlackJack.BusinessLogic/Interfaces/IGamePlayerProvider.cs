using System.Collections.Generic;
using BlackJack.Entities.Entities;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGamePlayerProvider
    {
        void CreateBets(IEnumerable<GamePlayer> bots, GamePlayer human, int bet, List<Log> logs);

        void PayRoundBets(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer);

        void CheckCardsInFirstTime(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer, List<Log> logs);

        void CheckCardsInSecondTime(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer, List<Log> logs);

        bool DoesHumanHaveEnoughCards(int roundScore);

        bool DoesBotHaveEnoughCards(int roundScore);

        string GetRoundResult(float betPayCoefficient);
    }
}
