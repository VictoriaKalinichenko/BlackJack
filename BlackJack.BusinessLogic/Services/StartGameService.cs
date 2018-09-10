using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
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
            string result = String.Empty;
            if (String.IsNullOrEmpty(name))
            {
                result = GameMessageHelper.NameFieldIsEmpty;
            }
            return result;
        }

        public async Task<string> PlayerCreation(string name)
        {
            Player human = await _playerRepository.SelectByName(name);
            if (human == null)
            {
                human = await CreatePlayer(name, PlayerType.Human);
            }

            return human.Name;
        }

        public async Task<AuthPlayerViewModel> PlayerAuthorization(string name)
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

        public async Task<int> CreateGame(int playerId, int amountOfBots)
        {
            int gameId = 0;

            Game game = new Game();
            game = await _gameRepository.Create(game);
            string message = $"Game(Id={gameId}, Stage={game.Stage}) is created";
            await _logRepository.Create(game.Id, message);

            List<Player> players = await CreatePlayerList(playerId, amountOfBots);
            foreach (Player player in players)
            {
                GamePlayer gamePlayer = new GamePlayer()
                {
                    GameId = game.Id,
                    PlayerId = player.Id,
                    Score = GameValueHelper.DefaultPlayerScore,
                    BetPayCoefficient = BetValueHelper.BetDefaultCoefficient,
                    Bet = BetValueHelper.BetZero,
                    RoundScore = 0,
                };

                await _gamePlayerRepository.Create(gamePlayer);
                
                message = $"{gamePlayer.Player.PlayerType}(Id={player.Id}, Name={player.Name}, Score={gamePlayer.Score}) is added to game";
                await _logRepository.Create(game.Id, message);
            }

            gameId = game.Id;
            return gameId;
        }

        public async Task<int> ResumeGame(int playerId)
        {
            int gameId = await _gamePlayerRepository.GetGameIdByPlayerId(playerId);
            return gameId;
        }

        public async Task<GameViewModel> GetGame(int gameId)
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.Bots = new List<PlayerViewModel>();
            gameViewModel.Id = gameId;
            Game game = await _gameRepository.Get(gameId);
            gameViewModel.Stage = game.Stage;

            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                PlayerViewModel playerViewModel = new PlayerViewModel();
                playerViewModel.Id = gamePlayer.Id;
                playerViewModel.Name = gamePlayer.Player.Name;
                playerViewModel.Score = gamePlayer.Score;

                if ((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer)
                {
                    gameViewModel.Dealer = playerViewModel;
                }

                if ((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Human)
                {
                    gameViewModel.Human = playerViewModel;
                    gameViewModel.Name = playerViewModel.Name;
                }

                if (!((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Human) && !((PlayerType)gamePlayer.Player.PlayerType == PlayerType.Dealer))
                {
                    gameViewModel.Bots.Add(playerViewModel);
                }
            }

            return gameViewModel;
        }

        private async Task<Player> CreatePlayer(string name, PlayerType playerType)
        {
            Player player = new Player();
            player.Name = name;
            player.PlayerType = (int)playerType;
            await _playerRepository.Create(player);
            return player;
        }

        private async Task<List<Player>> CreatePlayerList(int playerId, int amountOfBots)
        {
            List<Player> players = new List<Player>();

            players.Add(await _playerRepository.Get(playerId));

            Random random = new Random();
            Player dealer = await CreatePlayer(((BotName)random.Next(GameValueHelper.BotNameAmount)).ToString(), PlayerType.Dealer);
            players.Add(dealer);

            for (int i = 0; i < amountOfBots; i++)
            {
                Player bot = await CreatePlayer(((BotName)random.Next(GameValueHelper.BotNameAmount)).ToString(), PlayerType.Bot);
                players.Add(bot);
            }

            return players;
        }
    }
}