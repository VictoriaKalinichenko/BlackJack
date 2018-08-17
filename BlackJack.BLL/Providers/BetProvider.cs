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

        public float GetBetCoef(RoundResult roundResult)
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

        public int BetGenerate(int PlayerScore)
        {
            int bet = 0;

            Random random = new Random();
            bet = (random.Next(PlayerScore / Value.BetGenCoef) + 1) * Value.BetGenCoef;

            return bet;
        }
    }
}
