using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class AddCardRoundView
    {
        public string Name { get; set; }
        public int RoundScore { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public List<string> Cards { get; set; }
        public bool CanTakeCard { get; set; }
    }
}
