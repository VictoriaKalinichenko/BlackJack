using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class RestoreRoundView
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public GamePlayerRestoreRoundViewItem Dealer { get; set; }
        public GamePlayerRestoreRoundViewItem Human { get; set; }
        public List<GamePlayerRestoreRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerRestoreRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<string> Cards { get; set; }
    }
}
