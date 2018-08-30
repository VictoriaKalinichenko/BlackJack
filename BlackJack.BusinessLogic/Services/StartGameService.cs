using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;
using BlackJack.ViewModels.Enums;
using NLog;

namespace BlackJack.BusinessLogic.Services
{
    public class StartGameService : IStartGameService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly ILogRepository _logRepository;
        

        public StartGameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IGamePlayerRepository gamePlayerRepository, ILogRepository logRepository)
        {
            _gamePlayerRepository = gamePlayerRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _logRepository = logRepository;
        }
        
        public async Task<string> PlayerNameValidation(string name)
        {
            string result = String.Empty;

            try
            {
                if (await _playerRepository.SelectByName(name) != null)
                {
                    result = InputMessage.NameAlreadyExist;
                }

                if (String.IsNullOrEmpty(name))
                {
                    result = InputMessage.NameFieldIsEmpty;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }

            return result;
        }

        public async Task<int> CreateGame(NameAndBotAmountInputViewModel startInputViewModel)
        {
            try
            {
                int gameId = 0;

                Game game = new Game();
                game = await _gameRepository.Create(game);
                await _logRepository.CreateLogGameIsCreated(game.Id, game.Stage);

                List<Player> players = await CreatePlayerList(startInputViewModel.HumanName, startInputViewModel.AmountOfBots);
                foreach (Player player in players)
                {
                    GamePlayer gamePlayer = new GamePlayer()
                    {
                        GameId = game.Id,
                        PlayerId = player.Id,
                        Score = GameValue.DefaultPlayerScore,
                        BetPayCoefficient = BetValue.BetDefaultCoefficient,
                        Bet = BetValue.BetZero,
                        RoundScore = 0,
                    };

                    await _gamePlayerRepository.Create(gamePlayer);
                    await _logRepository.CreateLogPlayerIsAddedToGame(game.Id, player, gamePlayer.Score);
                }

                gameId = game.Id;

                return gameId;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
        
        private async Task<Player> CreatePlayer(string name, bool isHuman = false, bool isDealer = false)
        {
            try
            {
                Player player = new Player();
                player.Name = name;
                player.IsHuman = isHuman;
                player.IsDealer = isDealer;
                await _playerRepository.Create(player);
                return player;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task<List<Player>> CreatePlayerList(string name, int amountOfBots)
        {
            try
            {
                List<Player> players = new List<Player>();

                Player human = await _playerRepository.SelectByName(name);
                if (human == null)
                {
                    human = await CreatePlayer(name, true);
                }
                players.Add(human);

                Random random = new Random();
                Player dealer = await CreatePlayer(((BotName)random.Next(GameValue.BotNameAmount)).ToString(), false, true);
                players.Add(dealer);

                for (int i = 0; i < amountOfBots; i++)
                {
                    Player bot = await CreatePlayer(((BotName)random.Next(GameValue.BotNameAmount)).ToString());
                    players.Add(bot);
                }

                return players;
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
