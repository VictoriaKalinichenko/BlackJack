using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.CardFunctions
{
    public interface ICardWork
    {
        void AddCardToPlayer(Player player, Card card);

        void RemoveCardFromTable(Card card);
    }
}
