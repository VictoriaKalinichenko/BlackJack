using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Helpers
{
    public class RoundResult
    {
        public enum BetPayment
        {
            Continue = 0,
            IsBlackJack = 1,
            IsOneToOne = 2,
            IsBetReturning = 3,
            IsBetLossing = 4
        }
    }
}
