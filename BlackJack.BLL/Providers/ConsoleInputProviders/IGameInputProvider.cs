using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Providers.ConsoleInputProviders
{
    public interface IGameInputProvider
    {
        string InputName();

        int InputAmountOfBots();

        int InputBet(Player player);
    }
}
