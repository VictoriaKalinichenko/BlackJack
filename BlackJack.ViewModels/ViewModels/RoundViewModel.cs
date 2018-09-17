using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class RoundViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stage { get; set; }
        public PlayerItem Dealer { get; set; }
        public PlayerItem Human { get; set; }
        public List<PlayerItem> Bots { get; set; }
    }
}
