using System.Collections.Generic;

namespace BlackJack.ViewModels.Round
{
    public class AddCardRoundView
    {
        public string Name { get; set; }
        public int CardScore { get; set; }
        public List<string> Cards { get; set; }
        public bool CanTakeCard { get; set; }
    }
}
