﻿using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class ResponseStartRoundView
    {
        public long Id { get; set; }
        public bool CanTakeCard { get; set; }
        public bool BlackJackChoice { get; set; }
        public GamePlayerResponseStartRoundViewItem Dealer { get; set; }
        public GamePlayerResponseStartRoundViewItem Human { get; set; }
        public List<GamePlayerResponseStartRoundViewItem> Bots { get; set; }
    }

    public class GamePlayerResponseStartRoundViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
