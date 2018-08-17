using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }

        public List<PlayerViewModel> Players { get; set; } 
    }
}
