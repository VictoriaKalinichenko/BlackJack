using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Providers.BetProviders
{
    public interface IBetProvider
    {
        void CreateBet(Player player, int bet);

        void PayBj(Player player, Player dealer);

        void PayOneToOne(Player player, Player dealer);

        void BetReturning(Player player);

        void BetLossing(Player player, Player dealer);
    }
}
