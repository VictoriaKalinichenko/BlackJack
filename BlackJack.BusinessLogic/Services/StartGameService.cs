using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
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
        
        public string PlayerNameValidation(string name)
        {
            try
            {
                string result = String.Empty;
                if (String.IsNullOrEmpty(name))
                {
                    result = InputMessage.NameFieldIsEmpty;
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<string> PlayerCreation(string name)
        {
            try
            {
                Player human = await _playerRepository.SelectByName(name);
                if (human == null)
                {
                    human = await CreatePlayer(name, true);
                }

                return human.Name;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<AuthPlayerViewModel> PlayerAuthorization(string name)
        {
            try
            {
                Player human = await _playerRepository.SelectByName(name);

                bool resumeGame = true;
                int gameId = await _gamePlayerRepository.GetGameIdByPlayerId(human.Id);
                Game game = await _gameRepository.Get(gameId);
                if (game == null || !string.IsNullOrEmpty(game.Result))
                {
                    resumeGame = false;
                }

                AuthPlayerViewModel authPlayerViewModel = new AuthPlayerViewModel()
                {
                    PlayerId = human.Id,
                    Name = human.Name,
                    ResumeGame = resumeGame
                };

                return authPlayerViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<int> CreateGame(int playerId, int amountOfBots)
        {
            try
            {
                int gameId = 0;

                Game game = new Game();
                game = await _gameRepository.Create(game);
                await _logRepository.CreateLogGameIsCreated(game.Id, game.Stage);

                List<Player> players = await CreatePlayerList(playerId, amountOfBots);
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

        public async Task<int> ResumeGame(int playerId)
        {
            try
            {
                int gameId = await _gamePlayerRepository.GetGameIdByPlayerId(playerId);
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

        private async Task<List<Player>> CreatePlayerList(int playerId, int amountOfBots)
        {
            try
            {
                List<Player> players = new List<Player>();

                players.Add(await _playerRepository.Get(playerId));

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