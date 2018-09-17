using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class RoundViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stage { get; set; }
        public PlayerViewModel Dealer { get; set; }
        public PlayerViewModel Human { get; set; }
        public List<PlayerViewModel> Bots { get; set; }
    }
}
