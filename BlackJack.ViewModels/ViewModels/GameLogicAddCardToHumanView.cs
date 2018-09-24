using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicAddCardToHumanView
    {
        public string Name { get; set; }
        public int RoundScore { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public List<string> Cards { get; set; }
        public bool CanHumanTakeOneMoreCard { get; set; }
    }
}
