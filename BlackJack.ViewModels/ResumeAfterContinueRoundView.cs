using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class ResumeAfterContinueRoundView
    {
        public long Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerResumeAfterContinueRoundViewItem Dealer { get; set; }
        public GamePlayerResumeAfterContinueRoundViewItem Human { get; set; }
        public List<GamePlayerResumeAfterContinueRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerResumeAfterContinueRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
