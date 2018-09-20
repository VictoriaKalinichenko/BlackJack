using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicResumeGameAfterRoundSecondPhaseView
    {
        public int GameId { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerGameLogicResumeGameAfterRoundSecondPhaseItem Dealer { get; set; }
        public GamePlayerGameLogicResumeGameAfterRoundSecondPhaseItem Human { get; set; }
        public List<GamePlayerGameLogicResumeGameAfterRoundSecondPhaseItem> Bots { get; set; }
    }
}
