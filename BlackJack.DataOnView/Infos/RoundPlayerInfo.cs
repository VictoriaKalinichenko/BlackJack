using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataOnView.Infos
{
    public class RoundPlayerInfo : PlayerInfo
    {
        public int RoundScore { get; set; }

        public int Bet { get; set; }

        public List<string> Cards { get; set; }
    }
}
