using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class StartRoundView
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public GamePlayerStartRoundViewItem Dealer { get; set; }
        public GamePlayerStartRoundViewItem Human { get; set; }
        public List<GamePlayerStartRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerStartRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
