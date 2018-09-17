using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.ViewModels
{
    public class BetsCreationViewModel
    {
        public int Bet { get; set; }
        public int HumanGamePlayerId { get; set; }
        public int InGameId { get; set; }
    }
}
