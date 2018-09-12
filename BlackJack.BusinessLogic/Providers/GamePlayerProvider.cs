using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.Enums;
using BlackJack.DataAccess.Repositories.Interfaces;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        private readonly ILogRepository _logRepository;
        private IGamePlayerRepository _gamePlayerRepository;


        public GamePlayerProvider(IGamePlayerRepository gamePlayerRepository, ILogRepository logRepository)
        {
            _logRepository = logRepository;
            _gamePlayerRepository = gamePlayerRepository;
        }

        public async Task BetsCreation(IEnumerable<GamePlayer> inGamePlayers, int bet)
        {
            List<GamePlayer> gamePlayers = inGamePlayers.ToList();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if ((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Human)
                {
                    gamePlayer.Bet = bet;
                }

                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer) && !((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Human))
                {
                    gamePlayer.Bet = BetGenerate(gamePlayer.Score);
                }

                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer))
                {
                    gamePlayer.Score = gamePlayer.Score - gamePlayer.Bet;
                    await _gamePlayerRepository.Update(gamePlayer);
                    
                    string message = LogMessageHelper.BetCreated(gamePlayer.Player.PlayerType.ToString(), gamePlayer.Player.Id, gamePlayer.Player.Name, gamePlayer.Score, gamePlayer.Bet);
                    await _logRepository.Create(inGamePlayers.First().GameId, message);
                }
            }
        }

        public async Task RoundBetPayments(IEnumerable<GamePlayer> players)
        {
            GamePlayer human = players.Where(m => (PlayerType)m.Player.PlayerType == PlayerType.Human).FirstOrDefault();
            GamePlayer dealer = players.Where(m => (PlayerType)m.Player.PlayerType == PlayerType.Dealer).FirstOrDefault();

            foreach (GamePlayer player in players)
            {
                if (!((PlayerType)player.Player.PlayerType == PlayerType.Dealer) && player.BetPayCoefficient != BetValueHelper.BetDefaultCoefficient)
                {
                    BetPayment(player, dealer);
                    await _gamePlayerRepository.Update(player);
                    await _gamePlayerRepository.Update(dealer);
                }
            }
        }

        private void BetPayment(GamePlayer player, GamePlayer dealer)
        {
            int pay = (int)(player.Bet * player.BetPayCoefficient);

            player.Score += player.Bet + pay;
            player.Bet = BetValueHelper.BetZero;
            player.BetPayCoefficient = BetValueHelper.BetDefaultCoefficient;

            dealer.Score -= pay;
        }

        private int BetGenerate(int playerScore)
        {
            Random random = new Random();

            int maxBet = BetValueHelper.BotMaxBet;
            if (maxBet > playerScore)
            {
                maxBet = playerScore;
            }

            int bet = (random.Next(maxBet / BetValueHelper.BetGenCoef) + 1) * BetValueHelper.BetGenCoef;
            return bet;
        }
    }
}
