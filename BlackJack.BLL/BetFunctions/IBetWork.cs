using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.BetFunctions
{
    public interface IBetWork
    {
        void CreateBet(Player player, int bet);

        void PayBlackJack(Player player, Player dealer);

        void PayOneToOne(Player player, Player dealer);

        void ReturnBet(Player player);

        void LossingBet(Player player, Player dealer);
    }
}
