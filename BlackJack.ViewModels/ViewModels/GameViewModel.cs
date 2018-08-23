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

        public GamePlayerViewModel Dealer { get; set; }

        public GamePlayerViewModel Human { get; set; }

        public List<GamePlayerViewModel> Bots { get; set; }
    }
}
