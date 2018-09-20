using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GamePlayerGameLogicResumeGameAfterRoundFirstPhaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public List<string> Cards { get; set; }
    }
}
