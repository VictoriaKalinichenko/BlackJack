using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;

namespace BlackJack.BLL.BetFunctions
{
    public class BetWork : IBetWork
    {
        IUnitOfWork db;

        public BetWork(IUnitOfWork context)
        {
            db = context;
        }

        public void CreateBet(Player player, int bet)
        {
            player.Bet = bet;
            player.Score = player.Score - bet;
            UpdatePlayer(player);
        }

        public void PayBlackJack(Player player, Player dealer)
        {
            int pay = (int)(player.Bet * 1.5);

            player.Score += player.Bet + pay;
            player.Bet = 0;
            UpdatePlayer(player);
            
            dealer.Score -= pay;
            UpdatePlayer(dealer);
        }

        public void PayOneToOne(Player player, Player dealer)
        {
            int pay = player.Bet;

            player.Score += player.Bet + pay;
            player.Bet = 0;
            UpdatePlayer(player);
            
            dealer.Score -= pay;
            UpdatePlayer(dealer);
        }

        public void ReturnBet(Player player)
        {
            player.Score += player.Bet;
            player.Bet = 0;
            UpdatePlayer(player);
        }

        public void LossingBet(Player player, Player dealer)
        {
            int pay = player.Bet;
            
            player.Bet = 0;
            UpdatePlayer(player);
            
            dealer.Score += pay;
            UpdatePlayer(dealer);
        }

        void UpdatePlayer(Player player)
        {
            db.Players.Update(player);
            db.Save();
        }
    }
}
