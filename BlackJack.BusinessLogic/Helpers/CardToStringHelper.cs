using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class CardToStringHelper
    {
        public static string Convert(Card card)
        {
            string convertedString = string.Empty;
            convertedString = $"{((CardName)card.Name).ToString()} {((CardType)card.Type).ToString()}";
            return convertedString;
        }
    }
}
