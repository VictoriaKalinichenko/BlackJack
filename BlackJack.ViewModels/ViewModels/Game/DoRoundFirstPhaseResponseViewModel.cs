using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Game
{
    public class DoRoundFirstPhaseResponseViewModel
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public bool DealerBlackJackDanger { get; set; }
        public GamePlayerViewItem Dealer { get; set; }
        public GamePlayerViewItem Human { get; set; }
        public List<GamePlayerViewItem> Bots { get; set; }
    }
}
