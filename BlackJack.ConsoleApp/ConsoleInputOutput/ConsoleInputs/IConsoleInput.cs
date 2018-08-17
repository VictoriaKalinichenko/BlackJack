using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleInputs
{
    public interface IConsoleInput
    {
        string InputName();

        int InputAmountOfBots();

        int InputBet();

        int InputKeyForBjPayment(bool BjAndDealerBjDanger);

        int InputKeyForAddingCard();
    }
}
