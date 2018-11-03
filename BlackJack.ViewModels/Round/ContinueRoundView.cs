using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class ContinueRoundView
    {
        public long Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerContinueRoundViewItem Dealer { get; set; }
        public GamePlayerContinueRoundViewItem Human { get; set; }
        public List<GamePlayerContinueRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerContinueRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
