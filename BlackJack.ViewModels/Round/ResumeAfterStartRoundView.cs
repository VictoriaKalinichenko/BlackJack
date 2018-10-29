using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class ResumeAfterStartRoundView
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public bool BlackJackChoice { get; set; }
        public GamePlayerResumeAfterStartRoundViewItem Dealer { get; set; }
        public GamePlayerResumeAfterStartRoundViewItem Human { get; set; }
        public List<GamePlayerResumeAfterStartRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerResumeAfterStartRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
