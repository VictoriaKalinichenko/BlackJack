using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;

namespace BlackJack.BLL.Bet
{
    public class BjBet : IBet
    {
        IUnitOfWork db;

        Player player;
        Player dealer;


        public BjBet(IUnitOfWork context, Player _player, Player _dealer)
        {
            db = context;

            player = _player;
            dealer = _dealer;
        }


        public void Create(int bet)
        {
            player.Bet = bet;
            player.Score = player.Score - bet;
            UpdatePlayer(player);
        }

        public void PayBlackJack()
        {
            int pay = (int)(player.Bet * 1.5);

            player.Score += player.Bet + pay;
            player.Bet = 0;
            UpdatePlayer(player);

            dealer.Score -= pay;
            UpdatePlayer(dealer);
        }

        public void PayOneToOne()
        {
            int pay = player.Bet;

            player.Score += player.Bet + pay;
            player.Bet = 0;
            UpdatePlayer(player);

            dealer.Score -= pay;
            UpdatePlayer(dealer);
        }

        public void ReturnBet()
        {
            player.Score += player.Bet;
            player.Bet = 0;
            UpdatePlayer(player);
        }

        public void LossingBet()
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
