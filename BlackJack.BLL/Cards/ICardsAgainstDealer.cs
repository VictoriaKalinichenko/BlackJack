using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Cards
{
    public interface ICardsAgainstDealer
    {
        bool EqualsDealerScore();

        bool BetterThanDealerScore();
    }
}
