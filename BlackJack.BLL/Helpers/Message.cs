using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Helpers
{
    public static class Message
    {
        public static readonly string InputName = "Please, enter your name: ";

        public static readonly string NameFieldIsEmpty = "Name is not entered";

        public static readonly string NameAlreadyExist = "Player with that name already exist";

        public static readonly string NameDontExist = "Player with that name don't exist";


        public static readonly string BetMoreThanScore = "Entered bet more than player's score";

        public static readonly string InvalidAmountOfBots = "Invalid amount of bots";


        public static readonly string AppClosing = "Application is closing";

        public static readonly string IsNullOrEmpty = "String is null or empty";


        public static readonly string InputAmountOfBots = "Please, enter amount of bots: ";

        public static readonly string InputBet = "Please, enter your bet: ";


        public static readonly string DealerBjDanger = "Dealer black-jack danger";

        public static readonly string PlayerHasBj = " has black-jack";

        public static readonly string PlayerLost = " lost the round";

        public static readonly string PlayerScoreEqualsDealerScore = "'s score equals dealer's score";

        public static readonly string PlayerScoreBetterThanDealerScore = "'s score better than dealer's score";

        
        public static readonly string PlayerGetsBjPayment = " gets payment 3:2";

        public static readonly string PlayerGetsOneToOnePayment = " gets payment 1:1";

        public static readonly string BetGoesBackToPlayer = "Bet goes back to ";

        public static readonly string PlayerLostBet = " lost bet";


        public static readonly string Press0ToContinue = "Press 0 to continue the round";

        public static readonly string Press1ToTakeReward = "Press 1 to take reward and don't take part in the round";


        public static readonly string PressAnyKeyToContinue = "Press any key to continue";


        public static readonly string Press0ToEnough = "Press 0 if you have enough cards";

        public static readonly string Press1ToTakeCard = "Press 1 to take one more card";


        public static readonly string PlayerEndedTheGame = " ended the game";

        public static readonly string GameOver = "Game over";


        public static readonly string Press1ToStartNewGame = "Press 1 to start new game";

        public static readonly string Press0ToExit = "Press 0 to exit";


        public static readonly string DealerIsWinner = "Dealer is winner";

        public static readonly string DealerIsLoser = "Dealer is loser";


        public static readonly string Press0ToStartNewGame = "Press 0 to start new game";

        public static readonly string Press1ToResumeGame = "Press 1 to resume game";
    }
}
