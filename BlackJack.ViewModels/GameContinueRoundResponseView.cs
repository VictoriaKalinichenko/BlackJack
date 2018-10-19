using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Game
{
    public class ContinueRoundResponseViewModel
    {
        public long Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerItem Dealer { get; set; }
        public GamePlayerItem Human { get; set; }
        public List<GamePlayerItem> Bots { get; set; }
    }
}
