using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class StartGameStartRoundView
    {
        public long Id { get; set; }
        public int Stage { get; set; }
        public string IsGameOver { get; set; }
        public PlayerStartGameStartRoundItem Dealer { get; set; }
        public PlayerStartGameStartRoundItem Human { get; set; }
        public List<PlayerStartGameStartRoundItem> Bots { get; set; }
    }
}
