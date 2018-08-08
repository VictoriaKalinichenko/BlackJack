using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.GameMainClass
{
    public interface IGame
    {
        void Create(string name, int AmountOfBots);

        Player GetDealer();

        List<Player> GetBotList();

        void CreateBets(int HumanBet);

        void FirstCardsAdding();

        void AddOneMoreCardToPlayer(Player player);

        bool DealerFirstCardChecking(Player dealer);

        bool BlackJackChecking(Player player);

        bool DealerBlackJackChecking(Player dealer);

        bool CardSumMoreThan22(Player player);

        bool AllPlayersHaveZeroBets();
    }
}
