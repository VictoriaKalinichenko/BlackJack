using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.ViewModels
{
    public class BetInputViewModel
    {
        public GameStartViewModel Game { get; set; }

        public int HumanBet { get; set; }
    }
}
