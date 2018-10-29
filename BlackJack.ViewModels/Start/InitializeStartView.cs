using System.Collections.Generic;

namespace BlackJack.ViewModels.Start
{
    public class InitializeStartView
    {
        public long Id { get; set; }
        public int Stage { get; set; }
        public string IsGameOver { get; set; }
        public GamePlayerInitializeStartViewItem Dealer { get; set; }
        public GamePlayerInitializeStartViewItem Human { get; set; }
        public List<GamePlayerInitializeStartViewItem> Bots { get; set; }
    }

    public class GamePlayerInitializeStartViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
