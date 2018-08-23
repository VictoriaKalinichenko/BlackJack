using System;
using System.Collections.Generic;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Services
{
    public class StartGameService : IStartGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;

        public StartGameService()
        {
            _gamePlayerRepository = new GamePlayerRepository();
            _gameRepository = new GameRepository();
            _playerRepository = new PlayerRepository();
        }


        public string PlayerNameValidation(string name)
        {
            string result = String.Empty;
            
            if (_playerRepository.SelectByName(name) != null)
            {
                result = InputMessage._nameAlreadyExist;
            }

            if (String.IsNullOrEmpty(name))
            {
                result = InputMessage._nameFieldIsEmpty;
            }

            return result;
        }

        public int CreateGame(NameAndBotAmountInputViewModel startInputViewModel)
        {
            int gameId;

            Game game = new Game();
            game = _gameRepository.Create(game);

            List<Player> players = CreatePlayerList(startInputViewModel.HumanName, startInputViewModel.AmountOfBots);
            foreach (Player player in players)
            {
                GamePlayer gamePlayer = new GamePlayer()
                {
                    GameId = game.Id,
                    PlayerId = player.Id,
                    Score = GameValue._defaultPlayerScore,
                    BetPayCoefficient = BetValue._betDefaultCoefficient,
                    Bet = BetValue._betZero,
                    RoundScore = 0,
                };

                _gamePlayerRepository.Create(gamePlayer);
            }

            gameId = game.Id;
            return gameId;
        }
        
        private Player CreatePlayer(string name, bool isHuman = false, bool isDealer = false)
        {
            Player player = new Player();

            player.Name = name;
            player.IsHuman = isHuman;
            player.IsDealer = isDealer;
            _playerRepository.Create(player);

            return player;
        }

        private List<Player> CreatePlayerList(string name, int amountOfBots)
        {
            List<Player> players = new List<Player>();
            
            Player human = _playerRepository.SelectByName(name);
            if (human == null)
            {
                human = CreatePlayer(name, true);
            }
            players.Add(human);

            Random random = new Random();
            Player dealer = CreatePlayer(((BotName)random.Next(GameValue._botNameAmount)).ToString(), false, true);
            players.Add(dealer);

            for (int i = 0; i < amountOfBots; i++)
            {
                Player bot = CreatePlayer(((BotName)random.Next(GameValue._botNameAmount)).ToString());
                players.Add(bot);
            }

            return players;
        }
    }
}
