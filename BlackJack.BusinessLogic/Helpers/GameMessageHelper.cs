namespace BlackJack.BusinessLogic.Helpers
{
    public static class GameMessageHelper
    {
        public static readonly string DealerIsWinner = "Dealer is winner";
        public static readonly string DealerIsLoser = "Dealer is loser";
        public static readonly string Success = "SUCCESS";
        public static readonly string BetMoreThanScore = "Entered bet must be less than your score";
        public static readonly string BetLessThanMin = "Entered bet must be more than 0";

        public static readonly string NameInput = "Name: ";
        public static readonly string NameFieldIsEmpty = "Name is not entered";
        public static readonly string NameAlreadyExist = "Player with that name already exist";
        public static readonly string NameDontExist = "Player with that name don't exist";

        public static readonly string BotAmountInput = "Amount of bots: ";
        public static readonly string BetInput = "Please, enter your bet: ";

        public static readonly string PlayerAuthError = "Player authorization is not succeeded. Try again later";
        public static readonly string GameResumingError = "Game resuming is not succeeded. Try again later";
        public static readonly string GameCreationError = "Game creation is not succeeded. Try again later";
        public static readonly string GameLoadingError = "Game loading is not succeeded. Try again later";
        public static readonly string GameError = "The game proccessing is not succeeded. Try reload page";
        public static readonly string LogError = "The logs loading is not succeeded. Try reload page";
    }
}
