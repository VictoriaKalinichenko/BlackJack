using System;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class TableNameHelper
    {
        public static string GetTableName(Type entityName)
        {
            string tableName = $"{entityName.Name}s";
            return tableName;
        }
    }
}
