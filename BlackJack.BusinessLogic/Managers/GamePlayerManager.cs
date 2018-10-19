﻿using BlackJack.BusinessLogic.Helpers;
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
            if ((int)dealer.PlayerCards[0].Card.Rank >= CardValueHelper.DealerBlackJackDanger)
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
                    player.Bet = GameValueHelper.Zero;
                    player.BetPayCoefficient = BetValueHelper.DefaultCoefficient;
                    dealer.Score -= pay;
                }
            }
        }
        
        public string GetHumanRoundResult(float betPayCoefficient)
        {
            string roundResult = GameMessageHelper.Lose;

            if (betPayCoefficient == BetValueHelper.BlackJackCoefficient)
            {
                roundResult = GameMessageHelper.BlackJack;
            }

            if (betPayCoefficient == BetValueHelper.WinCoefficient)
            {
                roundResult = GameMessageHelper.Win;
            }

            if (betPayCoefficient == BetValueHelper.ZeroCoefficient)
            {
                roundResult = GameMessageHelper.ReturnBet;
            }

            return roundResult;
        }

        private float GetPayCoefficientAfterRoundStart(int roundScore, int amountOfCards, bool dealerBlackJackDanger)
        {
            float betPayCoefficient = BetValueHelper.DefaultCoefficient;

            if (!dealerBlackJackDanger && 
                roundScore == CardValueHelper.BlackJackScore &&
                amountOfCards == CardValueHelper.BlackJackAmount)
            {
                betPayCoefficient = BetValueHelper.BlackJackCoefficient;
            }

            if (dealerBlackJackDanger && 
                roundScore == CardValueHelper.BlackJackScore &&
                amountOfCards == CardValueHelper.BlackJackAmount)
            {
                betPayCoefficient = BetValueHelper.WinCoefficient;
            }

            return betPayCoefficient;
        }

        private float GetPayCoefficientAfterRoundContinue(int roundScore, int amountOfCards, 
            int dealerRoundScore, int dealerAmountOfCards, float betPayCoefficient)
        {
            if (betPayCoefficient == BetValueHelper.WinCoefficient)
            {
                return betPayCoefficient;
            }

            betPayCoefficient = BetValueHelper.LoseCoefficient;

            if ((roundScore == CardValueHelper.BlackJackScore &&
                amountOfCards == CardValueHelper.BlackJackAmount) &&
                !(dealerRoundScore == CardValueHelper.BlackJackScore &&
                dealerAmountOfCards == CardValueHelper.BlackJackAmount))
            {
                return BetValueHelper.BlackJackCoefficient;
            }

            if (roundScore <= CardValueHelper.BlackJackScore &&
                roundScore == dealerRoundScore)
            {
                betPayCoefficient = BetValueHelper.ZeroCoefficient;
            }

            if (roundScore <= CardValueHelper.BlackJackScore &&
                (roundScore > dealerRoundScore || dealerRoundScore > CardValueHelper.BlackJackScore))
            {
                betPayCoefficient = BetValueHelper.WinCoefficient;
            }

            return betPayCoefficient;
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
