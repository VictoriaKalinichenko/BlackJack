using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Cards;

namespace BlackJack.BLL.Players
{
    public interface IDealer
    {
        DealersCards Cards { get; }

        bool IsScoreNull();

        Player GetDealer();
    }
}
