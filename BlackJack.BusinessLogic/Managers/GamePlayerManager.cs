using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Managers
{
    public class GamePlayerManager : IGamePlayerManager
    {
        public void CreateBets(List<GamePlayer> players, int bet)
        {
            foreach (GamePlayer player in players)
            {
                if (player.Player.Type == PlayerType.Human)
                {
                    player.Bet = bet;
                }
                
                if (player.Player.Type == PlayerType.Bot)
                {
                    player.Bet = GenerateBet(player.Score);
                }

                if (!(player.Player.Type == PlayerType.Dealer))
                {
                    player.Score = player.Score - player.Bet;
                }
            }
        }

        public void DefinePayCoefficientsAfterRoundStart(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();

            bool dealerBlackJackDanger = false;
            if ((int)dealer.PlayerCards[0].Card.Rank >= CardValue.DealerBlackJackDanger)
            {
                dealerBlackJackDanger = true;
            }

            foreach (GamePlayer gamePlayer in players)
            {
                if (!(gamePlayer.Player.Type == PlayerType.Dealer))
                {
                    gamePlayer.BetPayCoefficient = GetPayCoefficientAfterRoundStart(gamePlayer.RoundScore,
                    gamePlayer.CardAmount, dealerBlackJackDanger);
                }
            }
        }

        public void DefinePayCoefficientsAfterRoundContinue(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();

            foreach (GamePlayer gamePlayer in players)
            {
                if (!(gamePlayer.Player.Type == PlayerType.Dealer))
                {
                    gamePlayer.BetPayCoefficient = GetPayCoefficientAfterRoundContinue(gamePlayer.RoundScore,
                        gamePlayer.CardAmount, dealer.RoundScore, dealer.CardAmount, gamePlayer.BetPayCoefficient);
                }
            }
        }

        public void PayBets(List<GamePlayer> players)
        {
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();

            foreach (GamePlayer player in players)
            {
                if (!(player.Player.Type == PlayerType.Dealer))
                {
                    int pay = (int)(player.Bet * player.BetPayCoefficient);
                    player.Score += player.Bet + pay;
                    player.Bet = 0;
                    player.BetPayCoefficient = BetValue.DefaultCoefficient;
                    dealer.Score -= pay;
                }
            }
        }
        
        public string GetHumanRoundResult(float betPayCoefficient)
        {
            string roundResult = GameMessage.Lose;

            if (betPayCoefficient == BetValue.BlackJackCoefficient)
            {
                roundResult = GameMessage.BlackJack;
            }

            if (betPayCoefficient == BetValue.WinCoefficient)
            {
                roundResult = GameMessage.Win;
            }

            if (betPayCoefficient == BetValue.ReturnCoefficient)
            {
                roundResult = GameMessage.ReturnBet;
            }

            return roundResult;
        }

        private float GetPayCoefficientAfterRoundStart(int roundScore, int amountOfCards, bool dealerBlackJackDanger)
        {
            float betPayCoefficient = BetValue.DefaultCoefficient;

            if (!dealerBlackJackDanger && 
                roundScore == CardValue.BlackJackScore &&
                amountOfCards == CardValue.BlackJackAmount)
            {
                betPayCoefficient = BetValue.BlackJackCoefficient;
            }

            if (dealerBlackJackDanger && 
                roundScore == CardValue.BlackJackScore &&
                amountOfCards == CardValue.BlackJackAmount)
            {
                betPayCoefficient = BetValue.WinCoefficient;
            }

            return betPayCoefficient;
        }

        private float GetPayCoefficientAfterRoundContinue(int roundScore, int amountOfCards, 
            int dealerRoundScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            if (betPayCoefficient == BetValue.WinCoefficient)
            {
                return betPayCoefficient;
            }

            betPayCoefficient = BetValue.LoseCoefficient;

            if ((roundScore == CardValue.BlackJackScore &&
                amountOfCards == CardValue.BlackJackAmount) &&
                !(dealerRoundScore == CardValue.BlackJackScore &&
                dealerAmountOfCards == CardValue.BlackJackAmount))
            {
                return BetValue.BlackJackCoefficient;
            }

            if (roundScore <= CardValue.BlackJackScore &&
                roundScore == dealerRoundScore)
            {
                betPayCoefficient = BetValue.ReturnCoefficient;
            }

            if (roundScore <= CardValue.BlackJackScore &&
                (roundScore > dealerRoundScore || dealerRoundScore > CardValue.BlackJackScore))
            {
                betPayCoefficient = BetValue.WinCoefficient;
            }

            return betPayCoefficient;
        }

        private int GenerateBet(int playerScore)
        {
            var random = new Random();

            int maxBet = BetValue.BotMaxBet;
            if (maxBet > playerScore)
            {
                maxBet = playerScore;
            }

            int bet = (random.Next(maxBet / BetValue.GenerationCoefficient) + 1) * BetValue.GenerationCoefficient;
            return bet;
        }
    }
}
