using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.ViewModels
{
    public class GamePlayerViewModel
    {
        public PlayerViewModel Player { get; set; }

        public int Score { get; set; }

        public int Bet { get; set; }

        public int CardScore { get; set; }

        public List<string> Cards { get; set; }
    }
}
