using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class EndRoundView
    {
        public string RoundResult { get; set; }
        public GamePlayerEndRoundViewItem Dealer { get; set; }
    }

    public class GamePlayerEndRoundViewItem
    {
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
