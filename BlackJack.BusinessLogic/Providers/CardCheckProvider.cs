using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;

namespace BlackJack.BusinessLogic.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        public bool DealerBjDanger(int firstCardValue)
        {
            bool danger = false;
            if (firstCardValue >= CardValueHelper.CardDealerBjDanger)
            {
                danger = true;
            }
            return danger;
        }

        public float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger)
        {
            float coef = BetValueHelper.BetDefaultCoefficient;

            if (!dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                coef = BetValueHelper.BetBjCoefficient;
            }

            if (dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                coef = BetValueHelper.BetWinCoefficient;
            }

            return coef;
        }

        public float RoundSecondPhaseResult(int bet, int score, int amountOfCards, int dealerScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            if (betPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                return betPayCoefficient;
            }

            if (bet == BetValueHelper.BetZero)
            {
                return BetValueHelper.BetDefaultCoefficient;
            }

            float coef = BetValueHelper.BetLoseCoefficient;

            if (PlayerBj(score, amountOfCards) && !PlayerBj(dealerScore, dealerAmountOfCards))
            {
                coef = BetValueHelper.BetBjCoefficient;
                return coef;
            }

            if (PlayerLossing(score))
            {
                coef = BetValueHelper.BetLoseCoefficient;
            }

            if (PlayerScoreEqualsDealerScore(score, dealerScore))
            {
                coef = BetValueHelper.BetZeroCoefficient;
            }

            if (PlayerScoreBetterThanDealerScore(score, dealerScore))
            {
                coef = BetValueHelper.BetWinCoefficient;
            }

            return coef;
        }

        public bool HumanPlayerHasEnoughCards(int score)
        {
            bool result = false;
            if (score >= CardValueHelper.CardBjScore)
            {
                result = true;
            }
            return result;
        }

        public bool BotHasEnoughCards(int score)
        {
            bool result = false;
            if (score >= CardValueHelper.CardBotEnoughScore)
            {
                result = true;
            }
            return result;
        }

        public string HumanRoundResult(float betPayCoefficient)
        {
            string humanRoundResult = string.Empty;

            if (betPayCoefficient == BetValueHelper.BetBjCoefficient)
            {
                humanRoundResult = RoundResultHelper.BlackJack;
            }

            if (betPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                humanRoundResult = RoundResultHelper.Win;
            }

            if (betPayCoefficient == BetValueHelper.BetZeroCoefficient)
            {
                humanRoundResult = RoundResultHelper.ReturnBet;
            }

            if (betPayCoefficient == BetValueHelper.BetLoseCoefficient)
            {
                humanRoundResult = RoundResultHelper.Lose;
            }

            return humanRoundResult;
        }

        private bool PlayerBj(int score, int amountOfCards)
        {
            bool result = false;
            if (score == CardValueHelper.CardBjScore && amountOfCards == CardValueHelper.CardBjAmount)
            {
                result = true;
            }
            return result;
        }

        private bool PlayerLossing(int score)
        {
            bool result = false;
            if (score > CardValueHelper.CardBjScore)
            {
                result = true;
            }
            return result;
        }

        private bool PlayerScoreEqualsDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;
            if (playerScore == dealerScore && !PlayerLossing(playerScore))
            {
                result = true;
            }
            return result;
        }

        private bool PlayerScoreBetterThanDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;
            if (!PlayerLossing(playerScore) && (playerScore > dealerScore || PlayerLossing(dealerScore)))
            {
                result = true;
            }
            return result;
        }
    }
}
