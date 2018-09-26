using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Game
{
    public class DoRoundSecondPhaseResponseViewModel
    {
        public long Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerViewItem Dealer { get; set; }
        public GamePlayerViewItem Human { get; set; }
        public List<GamePlayerViewItem> Bots { get; set; }
    }
}
