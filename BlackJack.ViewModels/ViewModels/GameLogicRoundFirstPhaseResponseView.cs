using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicRoundFirstPhaseResponseView
    {
        public int Id { get; set; }
        public bool CanHumanTakeOneMoreCard { get; set; }
        public bool HumanBlackJackAndDealerBlackJackDanger { get; set; }
        public GamePlayerGameLogicResponseItem Dealer { get; set; }
        public GamePlayerGameLogicResponseItem Human { get; set; }
        public List<GamePlayerGameLogicResponseItem> Bots { get; set; }
    }
}
