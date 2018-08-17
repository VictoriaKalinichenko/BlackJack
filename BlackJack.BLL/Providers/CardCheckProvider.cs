using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        public RoundResult RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger)
        {
            RoundResult roundResult = new RoundResult();
            roundResult = RoundResult.Continue;

            if (!dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                roundResult = RoundResult.IsBlackJack;
            }

            if (dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                roundResult = RoundResult.IsOneToOne;
            }

            return roundResult;
        }
        

        public bool DealerBjDanger(int firstCardValue)
        {
            bool danger = false;

            if (firstCardValue >= Value.CardDealerBjDanger)
            {
                danger = true;
            }

            return danger;
        }

        private bool PlayerBj(int score, int amountOfCards)
        {
            bool result = false;

            if (score == Value.CardBjScore && amountOfCards == Value.CardBjAmount)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerMoreThan21(int score)
        {
            bool result = false;

            if (score > Value.CardBjScore)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreEqualsDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;

            if (playerScore == dealerScore && !PlayerMoreThan21(playerScore))
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreBetterThanDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;

            if (!PlayerMoreThan21(playerScore) && (playerScore > dealerScore || PlayerMoreThan21(dealerScore)))
            {
                result = true;
            }

            return result;
        }
    }
}
