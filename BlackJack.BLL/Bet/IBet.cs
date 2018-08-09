using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Bet
{
    public interface IBet
    {
        void Create(int bet);

        void PayBlackJack();

        void PayOneToOne();

        void ReturnBet();

        void LossingBet();
    }
}
