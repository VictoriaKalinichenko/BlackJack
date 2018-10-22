using System;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class LogHelper
    {
        public static string ToString(Exception exception)
        {
            string message = $"{exception.Source}|{exception.TargetSite}|{exception.StackTrace}|{exception.Message}";
            return message;
        }
    }
}
