namespace BlackJack.BusinessLogic.Helpers
{
    public static class GameMessage
    {
        public static readonly string DealerIsWinner = "Dealer is winner";
        public static readonly string DealerIsLoser = "Dealer is loser";
        public static readonly string Success = "SUCCESS";
        public static readonly string BetMoreThanScore = "Entered bet must be less than your score";
        public static readonly string BetLessThanMin = "Entered bet must be more than 0";
    }
}
