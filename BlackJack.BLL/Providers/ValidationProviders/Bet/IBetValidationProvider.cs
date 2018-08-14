using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Providers.ValidationProviders.Bet
{
    public interface IBetValidationProvider
    {
        void Validate(Player player, int bet);
    }
}
