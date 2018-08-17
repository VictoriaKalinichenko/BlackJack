using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.Configuration;
using BlackJack.BLL.Services.Interfaces;
using BlackJack.BLL.Services;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.BLL.ViewModels;
using BlackJack.BLL.Helpers;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers
{
    public class BetProvider : IBetProvider
    {
        private IGameService GameService;


        public BetProvider ()
        {
            GameService = new GameService();
        }

        public void BetCreations(List<PlayerViewModel> players, int bet)
        {
            foreach (PlayerViewModel player in players)
            {
                if (player.Player.IsHuman)
                {
                    CreateBet(player, bet);
                }

                if (!player.Player.IsDealer && !player.Player.IsHuman)
                {
                    CreateBet(player, BetGenerate(player.GameScore.Score));
                }
            }
        }

        public void RoundBetPayments(List<PlayerViewModel> players, int oneToOnePayKey = 0)
        {
            PlayerViewModel human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();


            if (oneToOnePayKey == 1)
            {
                PayOneToOne(human, dealer);
                human.RoundResult = RoundResult.Continue;
            }

            foreach(PlayerViewModel player in players)
            {
                BetPayment(player, dealer);
            }
        }

        private void CreateBet(PlayerViewModel player, int bet)
        {
            player.GameScore.Bet = bet;
            player.GameScore.Score = player.GameScore.Score - bet;
            GameService.UpdatePlayerBetAndScore(player.GameScore);
        }

        private void BetPayment(PlayerViewModel player, PlayerViewModel dealer)
        {
            if (player.RoundResult == RoundResult.IsBlackJack)
            {
                PayBj(player, dealer);
            }

            if (player.RoundResult == RoundResult.IsOneToOne)
            {
                PayOneToOne(player, dealer);
            }

            if (player.RoundResult == RoundResult.IsBetLossing)
            {
                BetLossing(player, dealer);
            }

            if (player.RoundResult == RoundResult.IsBetReturning)
            {
                BetReturning(player);
            }
        }

        private void PayBj(PlayerViewModel player, PlayerViewModel dealer)
        {
            int pay = (int)(player.GameScore.Bet * Value.BetBjCoefficient);

            player.GameScore.Score += player.GameScore.Bet + pay;
            player.GameScore.Bet = Value.BetNull;
            GameService.UpdatePlayerBetAndScore(player.GameScore);
            
            dealer.GameScore.Score -= pay;
            GameService.UpdatePlayerBetAndScore(dealer.GameScore);
        }

        private void PayOneToOne(PlayerViewModel player, PlayerViewModel dealer)
        {
            int pay = (int)(player.GameScore.Bet * Value.BetWinCoefficient);

            player.GameScore.Score += player.GameScore.Bet + pay;
            player.GameScore.Bet = Value.BetNull;
            GameService.UpdatePlayerBetAndScore(player.GameScore);

            dealer.GameScore.Score -= pay;
            GameService.UpdatePlayerBetAndScore(dealer.GameScore);
        }

        private void BetReturning(PlayerViewModel player)
        {
            player.GameScore.Score += player.GameScore.Bet;
            player.GameScore.Bet = Value.BetNull;
            GameService.UpdatePlayerBetAndScore(player.GameScore);
        }

        private void BetLossing(PlayerViewModel player, PlayerViewModel dealer)
        {
            dealer.GameScore.Score += player.GameScore.Bet;
            GameService.UpdatePlayerBetAndScore(dealer.GameScore);

            player.GameScore.Bet = 0;
            GameService.UpdatePlayerBetAndScore(player.GameScore);
        }

        private int BetGenerate(int PlayerScore)
        {
            int bet = 0;

            Random random = new Random();
            bet = (random.Next(PlayerScore / Value.BetGenCoef) + 1) * Value.BetGenCoef;

            return bet;
        }
    }
}
