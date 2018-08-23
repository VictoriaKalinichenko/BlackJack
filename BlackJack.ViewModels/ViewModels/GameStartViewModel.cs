using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameStartViewModel
    {
        public int Id { get; set; }

        public GamePlayerStartViewModel Dealer { get; set; }

        public GamePlayerStartViewModel Human { get; set; }

        public List<GamePlayerStartViewModel> Bots { get; set; }
    }
}
