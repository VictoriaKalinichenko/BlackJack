using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using NLog;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IGamePlayerRepository _gamePlayerRepository;


        public GamePlayerProvider(IGamePlayerRepository gamePlayerRepository)
        {
            _gamePlayerRepository = gamePlayerRepository;
        }
        
        public async Task BetsCreation(IEnumerable<GamePlayer> inGamePlayers, int bet)
        {
            try
            {
                List<GamePlayer> gamePlayers = inGamePlayers.ToList();

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    if (gamePlayer.Player.IsHuman)
                    {
                        gamePlayer.Bet = bet;
                    }

                    if (!gamePlayer.Player.IsDealer && !gamePlayer.Player.IsHuman)
                    {
                        gamePlayer.Bet = BetGenerate(gamePlayer.Score);
                    }

                    if (!gamePlayer.Player.IsDealer)
                    {
                        gamePlayer.Score = gamePlayer.Score - gamePlayer.Bet;
                    }

                    await _gamePlayerRepository.Update(gamePlayer);
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task RoundBetPayments(IEnumerable<GamePlayer> players, int oneToOnePayKey = 0)
        {
            try
            {
                GamePlayer human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
                GamePlayer dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();

                if (oneToOnePayKey == 1)
                {
                    BetPayment(human, dealer);
                    human.BetPayCoefficient = BetValue.BetDefaultCoefficient;
                }

                foreach (GamePlayer player in players)
                {
                    if (!player.Player.IsDealer && player.BetPayCoefficient != BetValue.BetDefaultCoefficient)
                    {
                        BetPayment(player, dealer);
                        await _gamePlayerRepository.Update(player);
                        await _gamePlayerRepository.Update(dealer);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private void BetPayment(GamePlayer player, GamePlayer dealer)
        {
            try
            {
                int pay = (int)(player.Bet * player.BetPayCoefficient);

                player.Score += player.Bet + pay;
                player.Bet = BetValue.BetZero;

                dealer.Score -= pay;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private int BetGenerate(int playerScore)
        {
            try
            {
                Random random = new Random();

                int maxBet = BetValue.BotMaxBet;
                if (maxBet > playerScore)
                {
                    maxBet = playerScore;
                }

                int bet = (random.Next(maxBet / BetValue.BetGenCoef) + 1) * BetValue.BetGenCoef;
                return bet;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }
    }
}
