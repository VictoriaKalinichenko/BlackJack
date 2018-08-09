using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Bet;
using BlackJack.BLL.Cards;

namespace BlackJack.BLL.Players
{
    public interface IPlayer
    {
        IBet Bet { get; }

        PlayersCards Cards { get; }

        bool IsScoreNull();

        bool IsBetNull();

        Player GetPlayer();
    }
}
