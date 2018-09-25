﻿using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.GameLogic
{
    public class GamePlayerGameLogicResponseItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}