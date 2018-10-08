using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using System;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class ToStringHelper
    {
        public static string GetCardName(Card card)
        {
            string convertedString = string.Empty;
            convertedString = $"{((CardName)card.Name).ToString()} {((CardType)card.Type).ToString()}";
            return convertedString;
        }

        public static string GetTableName(Type entityName)
        {
            string tableName = $"{entityName.Name}s";
            return tableName;
        }
    }
}
