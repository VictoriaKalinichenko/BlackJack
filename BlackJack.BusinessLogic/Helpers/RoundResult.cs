namespace BlackJack.BusinessLogic.Helpers
{
    public static class RoundResult
    {
        public static readonly string BlackJack = "You have Blackjack and receive award 3:2";
        public static readonly string Win = "You are winner and receive award 1:1";
        public static readonly string ReturnBet = "Your score equals dealer's score and you receive your bet back";
        public static readonly string Lose = "You are loser and you lose your bet";
    }
}
