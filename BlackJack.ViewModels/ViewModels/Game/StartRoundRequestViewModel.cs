namespace BlackJack.ViewModels.ViewModels.Game
{
    public class StartRoundRequestViewModel
    {
        public int Bet { get; set; }
        public long GamePlayerId { get; set; }
        public long GameId { get; set; }
    }
}
