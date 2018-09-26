using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using System;
using System.Collections.Generic;

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

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerBlackJackResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
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

                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerBlackJackResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerWinResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient, dealer.RoundScore)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.ZeroCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = dealer.GameId,
                        Message = LogMessageHelper.PlayerEqualResult(((PlayerType)gamePlayer.Player.Type).ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient, dealer.RoundScore)
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.LoseCoefficient)
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
                player.Bet = GameValueHelper.Zero;
                player.BetPayCoefficient = BetValueHelper.DefaultCoefficient;
                dealer.Score -= pay;
            }
        }
        
        public bool DoesHumanHaveEnoughCards(int roundScore)
        {
            bool enoughCards = false;
            if (roundScore >= CardValueHelper.CardBlackJackScore)
            {
                enoughCards = true;
            }
            return enoughCards;
        }

        public bool DoesBotHaveEnoughCards(int roundScore)
        {
            bool enoughCards = false;
            if (roundScore >= CardValueHelper.CardBotEnoughScore)
            {
                enoughCards = true;
            }
            return enoughCards;
        }

        public string GetRoundResult(float betPayCoefficient)
        {
            string roundResult = RoundResultHelper.Lose;

            if (betPayCoefficient == BetValueHelper.BlackJackCoefficient)
            {
                roundResult = RoundResultHelper.BlackJack;
            }

            if (betPayCoefficient == BetValueHelper.WinCoefficient)
            {
                roundResult = RoundResultHelper.Win;
            }

            if (betPayCoefficient == BetValueHelper.ZeroCoefficient)
            {
                roundResult = RoundResultHelper.ReturnBet;
            }

            return roundResult;
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
            float betPayCoefficient = BetValueHelper.DefaultCoefficient;

            if (!dealerBlackJackDanger && IsPlayerBlackJack(roundScore, amountOfCards))
            {
                betPayCoefficient = BetValueHelper.BlackJackCoefficient;
            }

            if (dealerBlackJackDanger && IsPlayerBlackJack(roundScore, amountOfCards))
            {
                betPayCoefficient = BetValueHelper.WinCoefficient;
            }

            return betPayCoefficient;
        }

        private float GetRoundSecondPhaseResult(int roundScore, int amountOfCards, int dealerRoundScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            if (betPayCoefficient == BetValueHelper.WinCoefficient)
            {
                return betPayCoefficient;
            }

            betPayCoefficient = BetValueHelper.LoseCoefficient;

            if (IsPlayerBlackJack(roundScore, amountOfCards) && !IsPlayerBlackJack(dealerRoundScore, dealerAmountOfCards))
            {
                return BetValueHelper.BlackJackCoefficient;
            }

            if (DoesPlayerScoreEqualDealerScore(roundScore, dealerRoundScore))
            {
                betPayCoefficient = BetValueHelper.ZeroCoefficient;
            }

            if (IsPlayerScoreBetterThanDealerScore(roundScore, dealerRoundScore))
            {
                betPayCoefficient = BetValueHelper.WinCoefficient;
            }

            return betPayCoefficient;
        }

        private bool IsPlayerBlackJack(int roundScore, int amountOfCards)
        {
            bool blackJack = false;
            if (roundScore == CardValueHelper.CardBlackJackScore && amountOfCards == CardValueHelper.CardBlackJackAmount)
            {
                blackJack = true;
            }
            return blackJack;
        }

        private bool IsPlayerLossing(int roundScore)
        {
            bool playerLossing = false;
            if (roundScore > CardValueHelper.CardBlackJackScore)
            {
                playerLossing = true;
            }
            return playerLossing;
        }

        private bool DoesPlayerScoreEqualDealerScore(int playerRoundScore, int dealerRoundScore)
        {
            bool playerScoreEqualDealerScore = false;
            if (playerRoundScore == dealerRoundScore && !IsPlayerLossing(playerRoundScore))
            {
                playerScoreEqualDealerScore = true;
            }
            return playerScoreEqualDealerScore;
        }

        private bool IsPlayerScoreBetterThanDealerScore(int playerRoundScore, int dealerRoundScore)
        {
            bool playerScoreBetterThanDealerScore = false;
            if (!IsPlayerLossing(playerRoundScore) && (playerRoundScore > dealerRoundScore || IsPlayerLossing(dealerRoundScore)))
            {
                playerScoreBetterThanDealerScore = true;
            }
            return playerScoreBetterThanDealerScore;
        }

        private int GenerateBet(int playerScore)
        {
            var random = new Random();

            int maxBet = BetValueHelper.BotMaxBet;
            if (maxBet > playerScore)
            {
                maxBet = playerScore;
            }

            int bet = (random.Next(maxBet / BetValueHelper.GenerationCoefficient) + 1) * BetValueHelper.GenerationCoefficient;
            return bet;
        }
    }
}
