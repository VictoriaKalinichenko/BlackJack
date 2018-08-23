using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;

namespace BlackJack.BusinessLogic.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        public float RoundFirstPhaseResult(int score, int amountOfCards, int dealerFirstCard)
        {
            float coef = BetValue._betDefaultCoefficient;
            bool dealerBjDanger = DealerBjDanger(dealerFirstCard);

            if (!dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                coef = BetValue._betBjCoefficient;
            }

            if (dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                coef = BetValue._betWinCoefficient;
            }

            return coef;
        }
        
        private bool DealerBjDanger(int firstCardValue)
        {
            bool danger = false;

            if (firstCardValue >= CardValue._cardDealerBjDanger)
            {
                danger = true;
            }

            return danger;
        }

        private bool PlayerBj(int score, int amountOfCards)
        {
            bool result = false;

            if (score == CardValue._cardBjScore && amountOfCards == CardValue._cardBjAmount)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerMoreThan21(int score)
        {
            bool result = false;

            if (score > CardValue._cardBjScore)
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
