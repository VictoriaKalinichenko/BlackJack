using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class InitializeRoundStartView
    {
        public long Id { get; set; }
        public int Stage { get; set; }
        public string IsGameOver { get; set; }
        public GamePlayerInitializeRoundStartViewItem Dealer { get; set; }
        public GamePlayerInitializeRoundStartViewItem Human { get; set; }
        public List<GamePlayerInitializeRoundStartViewItem> Bots { get; set; }
    }

    public class GamePlayerInitializeRoundStartViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
