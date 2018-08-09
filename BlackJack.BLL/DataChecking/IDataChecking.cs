using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.DataChecking
{
    public interface IDataChecking
    {
        bool PlayerNameCheck(string name);

        bool BetCheck(Player player, int bet);
    }
}
