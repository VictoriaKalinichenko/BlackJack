﻿using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels.GameLogic
{
    public class GameLogicRoundFirstPhaseResponseView
    {
        public long Id { get; set; }
        public bool CanHumanTakeOneMoreCard { get; set; }
        public bool HumanBlackJackAndDealerBlackJackDanger { get; set; }
        public GamePlayerGameLogicResponseItem Dealer { get; set; }
        public GamePlayerGameLogicResponseItem Human { get; set; }
        public List<GamePlayerGameLogicResponseItem> Bots { get; set; }
    }
}