using System;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using NLog;

namespace BlackJack.BusinessLogic.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public bool DealerBjDanger(int firstCardValue)
        {
            try
            {
                bool danger = false;
                if (firstCardValue >= CardValue.CardDealerBjDanger)
                {
                    danger = true;
                }
                return danger;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger)
        {
            try
            {
                float coef = BetValue.BetDefaultCoefficient;

                if (!dealerBjDanger && PlayerBj(score, amountOfCards))
                {
                    coef = BetValue.BetBjCoefficient;
                }

                if (dealerBjDanger && PlayerBj(score, amountOfCards))
                {
                    coef = BetValue.BetWinCoefficient;
                }

                return coef;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public bool HumanPlayerHasEnoughCards(int score)
        {
            try
            {
                bool result = false;
                if (score >= CardValue.CardBjScore)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public bool BotHasEnoughCards(int score)
        {
            try
            {
                bool result = false;
                if (score >= CardValue.CardBotEnoughScore)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
        
        private bool PlayerBj(int score, int amountOfCards)
        {
            try
            {
                bool result = false;
                if (score == CardValue.CardBjScore && amountOfCards == CardValue.CardBjAmount)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private bool PlayerLossing(int score)
        {
            try
            {
                bool result = false;
                if (score > CardValue.CardBjScore)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private bool PlayerScoreEqualsDealerScore(int playerScore, int dealerScore)
        {
            try
            {
                bool result = false;
                if (playerScore == dealerScore && !PlayerLossing(playerScore))
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private bool PlayerScoreBetterThanDealerScore(int playerScore, int dealerScore)
        {
            try
            {
                bool result = false;
                if (!PlayerLossing(playerScore) && (playerScore > dealerScore || PlayerLossing(dealerScore)))
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
