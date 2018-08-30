﻿using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GamePlayerViewModel
    {
        public int Id { get; set; }

        public PlayerViewModel Player { get; set; }

        public int Score { get; set; }

        public int Bet { get; set; }

        public int CardScore { get; set; }

        public List<string> Cards { get; set; }
    }
}
