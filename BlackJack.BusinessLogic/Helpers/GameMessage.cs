namespace BlackJack.BusinessLogic.Helpers
{
    public static class GameMessage
    {
        public static readonly string DealerBjDanger = "Dealer black-jack danger";
        public static readonly string PlayerHasBj = " has black-jack";
        public static readonly string PlayerLost = " lost the round";
        public static readonly string PlayerScoreEqualsDealerScore = "'s score equals dealer's score";
        public static readonly string PlayerScoreBetterThanDealerScore = "'s score better than dealer's score";

        public static readonly string PlayerGetsBjPayment = " gets payment 3:2";
        public static readonly string PlayerGetsOneToOnePayment = " gets payment 1:1";
        public static readonly string BetGoesBackToPlayer = "Bet goes back to ";
        public static readonly string PlayerLostBet = " lost bet";

        public static readonly string PlayerEndedTheGame = " ended the game";
        public static readonly string GameOver = "Game over";
        public static readonly string DealerIsWinner = "Dealer is winner";
        public static readonly string DealerIsLoser = "Dealer is loser";
    }
}
