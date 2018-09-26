namespace BlackJack.BusinessLogic.Helpers
{
    public static class BetValueHelper
    {
        public static readonly float BlackJackCoefficient = 1.5F;
        public static readonly int WinCoefficient = 1;
        public static readonly int ZeroCoefficient = 0;
        public static readonly int LoseCoefficient = -1;
        public static readonly int DefaultCoefficient = 8;
        public static readonly int GenerationCoefficient = 50;
        public static readonly int BotMaxBet = 200;
    }
}
