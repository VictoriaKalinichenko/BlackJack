using System;
using System.Collections.Generic;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        public void CreateBets(IEnumerable<GamePlayer> bots, GamePlayer human, int bet, List<Log> logs)
        {
            human.Bet = bet;
            human.Score = human.Score - human.Bet;
            logs.Add(new Log()
            {
                GameId = human.GameId,
                Message = LogMessageHelper.BetCreated(((PlayerType)human.Player.Type).ToString(), human.Player.Id, human.Player.Name, human.Score, human.Bet)
            });

            foreach (GamePlayer bot in bots)
            {
                bot.Bet = GenerateBet(bot.Score);
                bot.Score = bot.Score - bot.Bet;
                logs.Add(new Log()
                {
                    GameId = bot.GameId,
                    Message = LogMessageHelper.BetCreated(((PlayerType)bot.Player.Type).ToString(), bot.Player.Id, bot.Player.Name, bot.Score, bot.Bet)
                });
            }
        }

        public void CheckCardsInFirstTime(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer, List<Log> logs)
        {
            bool dealerBlackJackDanger = IsDealerBlackJackDanger(dealer.PlayerCards[0].Card.Name);
            if (dealerBlackJackDanger)
            {
                logs.Add(new Log()
                {
                    GameId = dealer.GameId,
                    Message = LogMessageHelper.DealerBlackJackDanger(dealer.Id, dealer.Player.Name, dealer.PlayerCards[0].Card.Id, dealer.PlayerCards[0].Card.Name, CardToStringHelper.Convert(dealer.PlayerCards[0].Card))
                });
            }

            foreach (GamePlayer gamePlayer in humanAndBots)
            {
                gamePlayer.BetPayCoefficient = GetRoundFirstPhaseResult(gamePlayer.RoundScore, gamePlayer.CardAmount, dealerBlackJackDanger);

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetBlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerBlackJackResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerBlackJackAndDealerBlackJackDanger(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient)
                    });
                }
            }
        }

        public void CheckCardsInSecondTime(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer, List<Log> logs)
        {
            foreach (GamePlayer gamePlayer in humanAndBots)
            {
                gamePlayer.BetPayCoefficient = GetRoundSecondPhaseResult(gamePlayer.RoundScore, gamePlayer.CardAmount, dealer.RoundScore, dealer.CardAmount, gamePlayer.BetPayCoefficient);

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetBlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerBlackJackResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetWinCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerWinResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient, dealer.RoundScore)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetZeroCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerEqualResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient, dealer.RoundScore)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BetLoseCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerLoseResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient, dealer.RoundScore)
                    });
                }
            }
        }

        public void PayRoundBets(IEnumerable<GamePlayer> humanAndBots, GamePlayer dealer)
        {
            foreach (GamePlayer player in humanAndBots)
            {
                int pay = (int)(player.Bet * player.BetPayCoefficient);
                player.Score += player.Bet + pay;
                player.Bet = BetValueHelper.BetZero;
                player.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;
                dealer.Score -= pay;
            }
        }
        
        public bool DoesHumanHaveEnoughCards(int roundScore)
        {
            bool doesHumanHaveEnoughCards = false;
            if (roundScore >= CardValueHelper.CardBlackJackScore)
            {
                doesHumanHaveEnoughCards = true;
            }
            return doesHumanHaveEnoughCards;
        }

        public bool DoesBotHaveEnoughCards(int roundScore)
        {
            bool doesBotHaveEnoughCards = false;
            if (roundScore >= CardValueHelper.CardBotEnoughScore)
            {
                doesBotHaveEnoughCards = true;
            }
            return doesBotHaveEnoughCards;
        }

        public string GetHumanRoundResult(float betPayCoefficient)
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
        
        private bool IsDealerBlackJackDanger(int firstCardValue)
        {
            bool dealerBlackJackDanger = false;
            if (firstCardValue >= CardValueHelper.CardDealerBlackJackDanger)
            {
                dealerBlackJackDanger = true;
            }
            return dealerBlackJackDanger;
        }

        private float GetRoundFirstPhaseResult(int roundScore, int amountOfCards, bool dealerBlackJackDanger)
        {
            float betPayCoefficient = BetValueHelper.BetDefaultCoefficient;

            if (!dealerBlackJackDanger && IsPlayerBlackJack(roundScore, amountOfCards))
            {
                betPayCoefficient = BetValueHelper.BetBlackJackCoefficient;
            }

            if (dealerBlackJackDanger && IsPlayerBlackJack(roundScore, amountOfCards))
            {
                betPayCoefficient = BetValueHelper.BetWinCoefficient;
            }

            return betPayCoefficient;
        }

        private float GetRoundSecondPhaseResult(int roundScore, int amountOfCards, int dealerRoundScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            if (betPayCoefficient == BetValueHelper.BetWinCoefficient)
            {
                return betPayCoefficient;
            }

            betPayCoefficient = BetValueHelper.BetLoseCoefficient;

            if (IsPlayerBlackJack(roundScore, amountOfCards) && !IsPlayerBlackJack(dealerRoundScore, dealerAmountOfCards))
            {
                return BetValueHelper.BetBlackJackCoefficient;
            }

            if (DoesPlayerScoreEqualDealerScore(roundScore, dealerRoundScore))
            {
                betPayCoefficient = BetValueHelper.BetZeroCoefficient;
            }

            if (IsPlayerScoreBetterThanDealerScore(roundScore, dealerRoundScore))
            {
                betPayCoefficient = BetValueHelper.BetWinCoefficient;
            }

            return betPayCoefficient;
        }

        private bool IsPlayerBlackJack(int roundScore, int amountOfCards)
        {
            bool isPlayerBlackJack = false;
            if (roundScore == CardValueHelper.CardBlackJackScore && amountOfCards == CardValueHelper.CardBlackJackAmount)
            {
                isPlayerBlackJack = true;
            }
            return isPlayerBlackJack;
        }

        private bool IsPlayerLossing(int roundScore)
        {
            bool isPlayerLossing = false;
            if (roundScore > CardValueHelper.CardBlackJackScore)
            {
                isPlayerLossing = true;
            }
            return isPlayerLossing;
        }

        private bool DoesPlayerScoreEqualDealerScore(int playerRoundScore, int dealerRoundScore)
        {
            bool doesPlayerScoreEqualDealerScore = false;
            if (playerRoundScore == dealerRoundScore && !IsPlayerLossing(playerRoundScore))
            {
                doesPlayerScoreEqualDealerScore = true;
            }
            return doesPlayerScoreEqualDealerScore;
        }

        private bool IsPlayerScoreBetterThanDealerScore(int playerRoundScore, int dealerRoundScore)
        {
            bool isPlayerScoreBetterThanDealerScore = false;
            if (!IsPlayerLossing(playerRoundScore) && (playerRoundScore > dealerRoundScore || IsPlayerLossing(dealerRoundScore)))
            {
                isPlayerScoreBetterThanDealerScore = true;
            }
            return isPlayerScoreBetterThanDealerScore;
        }

        private int GenerateBet(int playerScore)
        {
            var random = new Random();

            int maxBet = BetValueHelper.BotMaxBet;
            if (maxBet > playerScore)
            {
                maxBet = playerScore;
            }

            int bet = (random.Next(maxBet / BetValueHelper.BetGenerationCoefficient) + 1) * BetValueHelper.BetGenerationCoefficient;
            return bet;
        }
    }
}
