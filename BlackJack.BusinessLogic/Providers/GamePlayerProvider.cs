using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        public void CreateBets(List<GamePlayer> players, int bet)
        {
            foreach (GamePlayer player in players)
            {
                if ((PlayerType)player.Player.Type == PlayerType.Human)
                {
                    player.Bet = bet;
                }
                
                if ((PlayerType)player.Player.Type == PlayerType.Bot)
                {
                    player.Bet = GenerateBet(player.Score);
                }

                if (!((PlayerType)player.Player.Type == PlayerType.Dealer))
                {
                    player.Score = player.Score - player.Bet;
                }
            }
        }

        public void DefinePayCoefficientsAfterRoundStart(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            bool dealerBlackJackDanger = IsDealerBlackJackDanger(dealer.PlayerCards[0].Card.Name);

            foreach (GamePlayer gamePlayer in players)
            {
                gamePlayer.BetPayCoefficient = GetPayCoefficientAfterRoundStart(gamePlayer.RoundScore, gamePlayer.CardAmount, dealerBlackJackDanger);
            }
        }

        public void DefinePayCoefficientsAfterRoundContinue(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            foreach (GamePlayer gamePlayer in players)
            {
                gamePlayer.BetPayCoefficient = GetPayCoefficientAfterRoundContinue(gamePlayer.RoundScore, gamePlayer.CardAmount, dealer.RoundScore, dealer.CardAmount, gamePlayer.BetPayCoefficient);
            }
        }

        public void PayBets(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            foreach (GamePlayer player in players)
            {
                int pay = (int)(player.Bet * player.BetPayCoefficient);
                player.Score += player.Bet + pay;
                player.Bet = GameValueHelper.Zero;
                player.BetPayCoefficient = BetValueHelper.DefaultCoefficient;
                dealer.Score -= pay;
            }
        }
        
        public bool IsEnoughCardsForHuman(int roundScore)
        {
            bool enoughCards = false;
            if (roundScore >= CardValueHelper.CardBlackJackScore)
            {
                enoughCards = true;
            }
            return enoughCards;
        }

        public bool IsEnoughCardsForBot(int roundScore)
        {
            bool enoughCards = false;
            if (roundScore >= CardValueHelper.CardBotEnoughScore)
            {
                enoughCards = true;
            }
            return enoughCards;
        }

        public string GetHumanRoundResult(float betPayCoefficient)
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

        private float GetPayCoefficientAfterRoundStart(int roundScore, int amountOfCards, bool dealerBlackJackDanger)
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

        private float GetPayCoefficientAfterRoundContinue(int roundScore, int amountOfCards, int dealerRoundScore, int dealerAmountOfCards, float betPayCoefficient)
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
