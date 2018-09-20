using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicResumeGameAfterRoundFirstPhaseView
    {
        public int GameId { get; set; }
        public bool CanHumanTakeOneMoreCard { get; set; }
        public bool HumanBlackJackAndDealerBlackJackDanger { get; set; }
        public GamePlayerGameLogicResumeGameAfterRoundFirstPhaseItem Dealer { get; set; }
        public GamePlayerGameLogicResumeGameAfterRoundFirstPhaseItem Human { get; set; }
        public List<GamePlayerGameLogicResumeGameAfterRoundFirstPhaseItem> Bots { get; set; }
    }
}
