using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.ViewModels;

namespace BlackJack.BLL.Providers.Interfaces
{
    public interface IBetProvider
    {
        void BetCreations(List<PlayerViewModel> players, int bet);

        void RoundBetPayments(List<PlayerViewModel> players, int oneToOnePayKey = 0);
    }
}
