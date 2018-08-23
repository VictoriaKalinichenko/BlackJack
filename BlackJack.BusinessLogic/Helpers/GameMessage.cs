namespace BlackJack.BusinessLogic.Helpers
{
    public static class GameMessage
    {
        public static readonly string _dealerBjDanger = "Dealer black-jack danger";
        public static readonly string _playerHasBj = " has black-jack";
        public static readonly string _playerLost = " lost the round";
        public static readonly string _playerScoreEqualsDealerScore = "'s score equals dealer's score";
        public static readonly string _playerScoreBetterThanDealerScore = "'s score better than dealer's score";

        public static readonly string _playerGetsBjPayment = " gets payment 3:2";
        public static readonly string _playerGetsOneToOnePayment = " gets payment 1:1";
        public static readonly string _betGoesBackToPlayer = "Bet goes back to ";
        public static readonly string _playerLostBet = " lost bet";

        public static readonly string _playerEndedTheGame = " ended the game";
        public static readonly string _gameOver = "Game over";
        public static readonly string _dealerIsWinner = "Dealer is winner";
        public static readonly string _dealerIsLoser = "Dealer is loser";
    }
}
