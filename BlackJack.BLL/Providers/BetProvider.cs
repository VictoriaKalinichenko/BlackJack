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
using BlackJack.ViewModels.ViewModels;
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
                    player.GameScore.Bet = bet;
                }

                if (!player.Player.IsDealer && !player.Player.IsHuman)
                {
                    player.GameScore.Bet = BetGenerate(player.GameScore.Score);
                }

                if (!player.Player.IsDealer)
                {
                    player.GameScore.Score = player.GameScore.Score - bet;
                    GameService.UpdatePlayerBetAndScore(player.GameScore);
                }
            }
        }

        public void RoundBetPayments(List<PlayerViewModel> players, int oneToOnePayKey = 0)
        {
            PlayerViewModel human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();


            if (oneToOnePayKey == 1)
            {
                BetPayment(human, dealer, Value.BetWinCoefficient);
                human.RoundResult = RoundResult.Continue;
            }

            foreach(PlayerViewModel player in players)
            {
                if (player.BetCoefficient != Value.BetDefaultCoefficient)
                {
                    BetPayment(player, dealer, player.BetCoefficient);
                }
            }
        }

        private float GetBetCoef(RoundResult roundResult)
        {
            float coef = Value.BetDefaultCoefficient;

            if (roundResult == RoundResult.IsBlackJack)
            {
                coef = Value.BetBjCoefficient;
            }

            if (roundResult == RoundResult.IsOneToOne)
            {
                coef = Value.BetWinCoefficient;
            }

            if (roundResult == RoundResult.IsBetLossing)
            {
                coef = Value.BetLoseCoefficient;
            }

            if (roundResult == RoundResult.IsBetReturning)
            {
                coef = Value.BetNullCoefficient;
            }

            return coef;
        }

        private void BetPayment(PlayerViewModel player, PlayerViewModel dealer, float betCoef)
        {
            int pay = (int)(player.GameScore.Bet * betCoef);

            player.GameScore.Score += player.GameScore.Bet + pay;
            player.GameScore.Bet = Value.BetNull;
            GameService.UpdatePlayerBetAndScore(player.GameScore);

            dealer.GameScore.Score -= pay;
            GameService.UpdatePlayerBetAndScore(dealer.GameScore);
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
