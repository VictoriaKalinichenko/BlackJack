using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class TakeCardRoundView
    {
        public string RoundResult { get; set; }
        public GamePlayerTakeCardRoundViewItem Dealer { get; set; }
        public GamePlayerTakeCardRoundViewItem Human { get; set; }
        public List<GamePlayerTakeCardRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerTakeCardRoundViewItem
    {
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
