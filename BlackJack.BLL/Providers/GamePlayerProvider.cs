using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;

namespace BlackJack.BusinessLogic.Providers
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        private IGamePlayerRepository _gamePlayerRepository;

        public GamePlayerProvider()
        {
            _gamePlayerRepository = new GamePlayerRepository();
        }


        public List<GamePlayer> BetsCreation(List<GamePlayer> players, int bet)
        {
            List<GamePlayer> gamePlayers = new List<GamePlayer>();

            foreach (GamePlayer gamePlayer in players)
            {
                Player player = _gamePlayerRepository.GetPlayerByGamePlayerId(gamePlayer.Id);

                if (player.IsHuman)
                {
                    gamePlayer.Bet = bet;
                }

                if (!player.IsDealer && !player.IsHuman)
                {
                    gamePlayer.Bet = BetGenerate(gamePlayer.Score);
                }

                if (!player.IsDealer)
                {
                    gamePlayer.Score = gamePlayer.Score - bet;
                }

                _gamePlayerRepository.Update(gamePlayer);
            }

            return gamePlayers;
        }

        public void RoundBetPayments(List<GamePlayer> players, int oneToOnePayKey = 0)
        {
            GamePlayer human = players.Where(m => m.Player.IsHuman).FirstOrDefault();
            GamePlayer dealer = players.Where(m => m.Player.IsDealer).FirstOrDefault();


            if (oneToOnePayKey == 1)
            {
                BetPayment(human, dealer);
                human.BetPayCoefficient = BetValue._betDefaultCoefficient;
            }

            foreach (GamePlayer player in players)
            {
                if (!player.Player.IsDealer && player.BetPayCoefficient != BetValue._betDefaultCoefficient)
                {
                    BetPayment(player, dealer);
                    _gamePlayerRepository.Update(player);
                    _gamePlayerRepository.Update(dealer);
                }

            }
        }

        private void BetPayment(GamePlayer player, GamePlayer dealer)
        {
            int pay = (int)(player.Bet * player.BetPayCoefficient);

            player.Score += player.Bet + pay;
            player.Bet = BetValue._betZero;

            dealer.Score -= pay;
        }

        private int BetGenerate(int PlayerScore)
        {
            int bet = 0;

            Random random = new Random();
            bet = (random.Next(PlayerScore / BetValue._betGenCoef) + 1) * BetValue._betGenCoef;

            return bet;
        }
    }
}
