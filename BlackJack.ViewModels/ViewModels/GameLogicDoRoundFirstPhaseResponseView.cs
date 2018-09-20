using System.Collections.Generic;

namespace BlackJack.ViewModels.ViewModels
{
    public class GameLogicDoRoundFirstPhaseResponseView
    {
        public int GameId { get; set; }
        public bool CanHumanTakeOneMoreCard { get; set; }
        public bool HumanBlackJackAndDealerBlackJackDanger { get; set; }
        public GamePlayerGameLogicDoRoundFirstPhaseResponseItem Dealer { get; set; }
        public GamePlayerGameLogicDoRoundFirstPhaseResponseItem Human { get; set; }
        public List<GamePlayerGameLogicDoRoundFirstPhaseResponseItem> Bots { get; set; }
    }
}
