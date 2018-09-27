using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Game
{
    public class StartRoundResponseViewModel
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public bool BlackJackChoice { get; set; }
        public GamePlayerItem Dealer { get; set; }
        public GamePlayerItem Human { get; set; }
        public List<GamePlayerItem> Bots { get; set; }
    }
}
