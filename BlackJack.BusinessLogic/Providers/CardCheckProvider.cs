using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;

namespace BlackJack.BusinessLogic.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        public bool DealerBlackJackDanger(int firstCardValue)
        {
            bool danger = false;
            if (firstCardValue >= CardValueHelper.CardDealerBlackJackDanger)
            {
                danger = true;
            }
            return danger;
        }

        public float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBlackJackDanger)
        {
            float coef = BetValueHelper.BetDefaultCoefficient;

            if (!dealerBlackJackDanger && PlayerBlackJack(score, amountOfCards))
            {
                coef = BetValueHelper.BetBlackJackCoefficient;
            }

            if (dealerBlackJackDanger && PlayerBlackJack(score, amountOfCards))
            {
                coef = BetValueHelper.BetWinCoefficient;
            }

            return coef;
        }

        public float RoundSecondPhaseResult(int score, int amountOfCards, int dealerScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            float coef = BetValueHelper.BetLoseCoefficient;

            if (betPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                return betPayCoefficient;
            }

            if (PlayerBlackJack(score, amountOfCards) && !PlayerBlackJack(dealerScore, dealerAmountOfCards))
            {
                return BetValueHelper.BetBlackJackCoefficient;
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
            if (score >= CardValueHelper.CardBlackJackScore)
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
            string humanRoundResult = RoundResultHelper.Lose;

            if (betPayCoefficient == BetValueHelper.BetBlackJackCoefficient)
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

            return humanRoundResult;
        }

        private bool PlayerBlackJack(int score, int amountOfCards)
        {
            bool result = false;
            if (score == CardValueHelper.CardBlackJackScore && amountOfCards == CardValueHelper.CardBlackJackAmount)
            {
                result = true;
            }
            return result;
        }

        private bool PlayerLossing(int score)
        {
            bool result = false;
            if (score > CardValueHelper.CardBlackJackScore)
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
