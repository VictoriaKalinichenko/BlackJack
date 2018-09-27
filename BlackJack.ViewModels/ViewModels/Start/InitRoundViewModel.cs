using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.Start
{
    public class InitRoundViewModel
    {
        public long Id { get; set; }
        public int Stage { get; set; }
        public string IsGameOver { get; set; }
        public PlayerItem Dealer { get; set; }
        public PlayerItem Human { get; set; }
        public List<PlayerItem> Bots { get; set; }

        public class PlayerItem
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}
