namespace BlackJack.BusinessLogic.Helpers
{
    public static class BetValueHelper
    {
        public static readonly float BetBlackJackCoefficient = 1.5F;
        public static readonly int BetWinCoefficient = 1;
        public static readonly int BetZeroCoefficient = 0;
        public static readonly int BetLoseCoefficient = -1;
        public static readonly int BetDefaultCoefficient = 8;
        public static readonly int BetGenerationCoefficient = 50;
        public static readonly int BetZero = 0;
        public static readonly int BotMaxBet = 200;
    }
}
