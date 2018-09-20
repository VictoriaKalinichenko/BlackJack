using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicDoRoundSecondPhaseResponseView
    {
        public int GameId { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerGameLogicDoRoundSecondPhaseResponseItem Dealer { get; set; }
        public GamePlayerGameLogicDoRoundSecondPhaseResponseItem Human { get; set; }
        public List<GamePlayerGameLogicDoRoundSecondPhaseResponseItem> Bots { get; set; }
    }
}
