using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers.Interfaces
{
    public interface ICardCheckProvider
    {
        RoundResult RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger);

        bool DealerBjDanger(int firstCardValue);
    }
}
