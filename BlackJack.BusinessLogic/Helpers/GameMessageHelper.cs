﻿namespace BlackJack.BusinessLogic.Helpers
{
    public static class GameMessageHelper
    {
        public static readonly string DealerIsWinner = "Dealer is winner";
        public static readonly string DealerIsLoser = "Dealer is loser";
        public static readonly string Success = "SUCCESS";
        public static readonly string BetIsNotValid = "Bet is not valid";

        public static readonly string NameFieldIsEmpty = "Name is not entered";

        public static readonly string PlayerAuthError = "Player authorization is not succeeded. Try again later";
        public static readonly string GameResumingError = "Game resuming is not succeeded. Try again later";
        public static readonly string GameCreationError = "Game creation is not succeeded. Try again later";
        public static readonly string GameLoadingError = "Game loading is not succeeded. Try again later";
        public static readonly string GameError = "The game proccessing is not succeeded. Try reload page";
        public static readonly string LogError = "The logs loading is not succeeded. Try reload page";
        public static readonly string ReceivedDataError = "Received data is not valid";

        public static readonly string BlackJack = "You have Blackjack and receive award 3:2";
        public static readonly string Win = "You are winner and receive award 1:1";
        public static readonly string ReturnBet = "Your score equals dealer's score and you receive your bet back";
        public static readonly string Lose = "You are loser and you lose your bet";
    }
}
