using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Helpers
{
    public static class Value
    {
        public static readonly int CardAmount = 52;
        public static readonly int CardTypeAmount = 4;
        public static readonly int CardDealerBjDanger = 10;
        public static readonly int CardBjAmount = 2;
        public static readonly int CardBjScore = 21;

        public static readonly float BetBjCoefficient = 1.5F;
        public static readonly int BetWinCoefficient = 1;
        public static readonly int BetNullCoefficient = 0;
        public static readonly int BetLoseCoefficient = -1;
        public static readonly int BetDefaultCoefficient = 8;
        public static readonly int BetGenCoef = 50;
        public static readonly int BetNull = 0;
    }
}
