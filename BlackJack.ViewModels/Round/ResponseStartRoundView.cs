using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class ResponseStartRoundView
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public bool BlackJackChoice { get; set; }
        public GamePlayerStartRoundViewItem Dealer { get; set; }
        public GamePlayerStartRoundViewItem Human { get; set; }
        public List<GamePlayerStartRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerStartRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
