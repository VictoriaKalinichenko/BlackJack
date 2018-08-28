namespace BlackJack.BusinessLogic.Helpers
{
    public static class BetValue
    {
        public static readonly float BetBjCoefficient = 1.5F;
        public static readonly int BetWinCoefficient = 1;
        public static readonly int BetZeroCoefficient = 0;
        public static readonly int BetLoseCoefficient = -1;
        public static readonly int BetDefaultCoefficient = 8;
        public static readonly int BetGenCoef = 50;
        public static readonly int BetZero = 0;
        public static readonly int BotMaxBet = 200;
    }
}
