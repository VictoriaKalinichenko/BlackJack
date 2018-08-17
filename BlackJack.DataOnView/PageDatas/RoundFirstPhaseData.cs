using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DataOnView.Infos;

namespace BlackJack.DataOnView.PageDatas
{
    public class RoundFirstPhaseData
    {
        public RoundFirstPhaseDealerInfo Dealer { get; set; }

        public RoundPlayerInfo Human { get; set; }

        public List<RoundPlayerInfo> Bots { get; set; }

        public List<string> RoundMessages { get; set; }
    }
}
