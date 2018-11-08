using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class StartRoundView
    {
        public string RoundResult { get; set; }
        public GamePlayerStartRoundViewItem Dealer { get; set; }
        public GamePlayerStartRoundViewItem Human { get; set; }
        public List<GamePlayerStartRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerStartRoundViewItem
    {
        public string Name { get; set; }
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
