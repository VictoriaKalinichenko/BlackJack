using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class RestoreRoundView
    {
        public bool CanTakeCard { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerRestoreRoundViewItem Dealer { get; set; }
        public GamePlayerRestoreRoundViewItem Human { get; set; }
        public List<GamePlayerRestoreRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerRestoreRoundViewItem
    {
        public string Name { get; set; }
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
