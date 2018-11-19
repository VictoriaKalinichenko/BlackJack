using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class ResponseStartRoundView
    {
        public string RoundResult { get; set; }
        public GamePlayerStartRoundViewItem Dealer { get; set; }
        public GamePlayerStartRoundViewItem Human { get; set; }
        public List<GamePlayerStartRoundViewItem> Bots { get; set; }

        public ResponseStartRoundView()
        {
            Bots = new List<GamePlayerStartRoundViewItem>();
        }
    }

    public class GamePlayerStartRoundViewItem
    {
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
