using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.Configuration;

namespace BlackJack.BLL.Providers.BetProviders
{
    public class BetProvider : IBetProvider
    {
        public void CreateBet(Player player, int bet)
        {
            player.Bet = bet;
            player.Score = player.Score - bet;
            Config.db.Players.Update(player);
        }

        public void PayBj(Player player, Player dealer)
        {
            int pay = (int)(player.Bet * 1.5);

            player.Score += player.Bet + pay;
            player.Bet = 0;
            Config.db.Players.Update(player);
            
            dealer.Score -= pay;
            Config.db.Players.Update(dealer);
        }

        public void PayOneToOne(Player player, Player dealer)
        {
            int pay = player.Bet;

            player.Score += player.Bet + pay;
            player.Bet = 0;
            Config.db.Players.Update(player);
            
            dealer.Score -= pay;
            Config.db.Players.Update(dealer);
        }

        public void BetReturning(Player player)
        {
            player.Score += player.Bet;
            player.Bet = 0;
            Config.db.Players.Update(player);
        }

        public void BetLossing(Player player, Player dealer)
        {
            dealer.Score += player.Bet;
            Config.db.Players.Update(dealer);

            player.Bet = 0;
            Config.db.Players.Update(player);
        }
    }
}
