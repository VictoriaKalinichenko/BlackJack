using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.Enums
{
    public enum RoundResult
    {
        Continue = 0,
        IsBlackJack = 1,
        IsOneToOne = 2,
        IsBetReturning = 3,
        IsBetLossing = 4
    }
}
