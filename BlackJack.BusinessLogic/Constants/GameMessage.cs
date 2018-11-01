namespace BlackJack.BusinessLogic.Constants
{
    public static class GameMessage
    {
        public static readonly string PlayerAuthError = "Player authorization is not succeeded. Try again later";
        public static readonly string GameResumingError = "Game restoring is not succeeded. Try again later";
        public static readonly string GameCreationError = "Game creation is not succeeded. Try again later";
        public static readonly string GameLoadingError = "Game loading is not succeeded. Try again later";
        public static readonly string GameError = "The game proccessing is not succeeded. Try reload page";
        public static readonly string HistoryMessagesError = "The history messages loading is not succeeded. Try reload page";
        public static readonly string ReceivedDataError = "Received data is not valid";
        
        public static readonly string Win = "You are winner";
        public static readonly string Equal = "Your score equals dealer's score";
        public static readonly string Lose = "Dealer is winner";
    }
}