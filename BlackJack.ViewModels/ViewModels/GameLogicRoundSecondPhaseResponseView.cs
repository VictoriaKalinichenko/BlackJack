using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicRoundSecondPhaseResponseView
    {
        public int Id { get; set; }
        public string RoundResult { get; set; }
        public GamePlayerGameLogicResponseItem Dealer { get; set; }
        public GamePlayerGameLogicResponseItem Human { get; set; }
        public List<GamePlayerGameLogicResponseItem> Bots { get; set; }
    }
}
