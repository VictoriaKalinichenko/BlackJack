using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DataOnView.Infos;

namespace BlackJack.DataOnView.PageDatas
{
    public class RoundStartPageData
    {
        public PlayerInfo Dealer { get; set; }

        public PlayerInfo Human { get; set; }

        public List<PlayerInfo> Bots { get; set; }
    }
}
