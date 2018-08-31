using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public PlayerViewModel Dealer { get; set; }

        public PlayerViewModel Human { get; set; }

        public List<PlayerViewModel> Bots { get; set; }
    }
}
