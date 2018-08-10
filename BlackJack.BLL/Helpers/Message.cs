using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Helpers
{
    public static class Message
    {
        public static readonly string NameFieldIsEmpty = "Name is not entered";

        public static readonly string NameAlreadyExist = "Player with that name already exist";

        public static readonly string BetMoreThanScore = "Entered bet more than player's score";

        public static readonly string InvalidAmountOfBots = "Invalid amount of bots";

        public static readonly string InputName = "Please, enter your name: ";

        public static readonly string TryAgain = "Try again";

        public static readonly string InputAmountOfBots = "Please, enter amount of bots: ";

        public static readonly string InputBet = "Please, enter your bet: ";
    }
}
