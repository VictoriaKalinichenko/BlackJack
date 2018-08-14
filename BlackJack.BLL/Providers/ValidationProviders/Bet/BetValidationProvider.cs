using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Infrastructure;
using BlackJack.BLL.Helpers;

namespace BlackJack.BLL.Providers.ValidationProviders.Bet
{
    public class BetValidationProvider : IBetValidationProvider
    {
        public void Validate(Player player, int bet)
        {
            if (player.Score < bet)
            {
                throw new ValidationException(Message.BetMoreThanScore);
            }
        }
    }
}
