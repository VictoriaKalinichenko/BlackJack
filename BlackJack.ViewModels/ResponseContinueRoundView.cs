using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class ResponseContinueRoundView
    {
        public long Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerResponseContinueRoundViewItem Dealer { get; set; }
        public GamePlayerResponseContinueRoundViewItem Human { get; set; }
        public List<GamePlayerResponseContinueRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerResponseContinueRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
