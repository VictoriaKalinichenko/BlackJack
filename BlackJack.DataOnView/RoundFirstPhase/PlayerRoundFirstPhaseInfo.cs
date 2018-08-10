using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataOnView.RoundFirstPhase
{
    public class PlayerRoundFirstPhaseInfo
    {
        public PlayerInfo PlayerInfo { get; set; }

        public List<string> Cards { get; set; }

        public int RoundScore { get; set; }

        public int Bet { get; set; }
    }
}
