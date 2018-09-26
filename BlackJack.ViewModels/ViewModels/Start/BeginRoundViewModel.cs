using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Start
{
    public class BeginRoundViewModel
    {
        public long Id { get; set; }
        public int Stage { get; set; }
        public string IsGameOver { get; set; }
        public PlayerBeginRoundViewItem Dealer { get; set; }
        public PlayerBeginRoundViewItem Human { get; set; }
        public List<PlayerBeginRoundViewItem> Bots { get; set; }
    }
}
