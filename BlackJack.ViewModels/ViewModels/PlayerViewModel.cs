using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BlackJack.BLL.Helpers;
using BlackJack.Entity.Models;
using BlackJack.Entity.Enums;

namespace BlackJack.ViewModels.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }

        public GamePlayer GameScore { get; set; }

        //public List<Card> Cards { get; set; }

        public RoundResult RoundResult { get; set; }

        public float BetCoefficient { get; set; }
    }
}
