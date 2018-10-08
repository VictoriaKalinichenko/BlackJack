using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class StartService : IStartService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly ILogRepository _logRepository;
        

        public StartService(IGameRepository gameRepository, IPlayerRepository playerRepository, 
            IGamePlayerRepository gamePlayerRepository, ILogRepository logRepository)
        {
            _gamePlayerRepository = gamePlayerRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _logRepository = logRepository;
        }

        public async Task CreatePlayer(string name)
        {
            Player human = await _playerRepository.SelectByName(name, (int)PlayerType.Human);
            if (human == null)
            {
                human = CreatePlayer(name, PlayerType.Human);
                await _playerRepository.Create(human);
            }
        }

        public async Task<AuthorizePlayerViewModel> AuthorizePlayer(string name)
        {
            Player human = await _playerRepository.SelectByName(name, (int)PlayerType.Human);

            bool resumeGame = true;
            Game game = await _gameRepository.GetByPlayerId(human.Id);
            if (game == null || !string.IsNullOrEmpty(game.Result))
            {
                resumeGame = false;
            }

            var authorizePlayerViewModel = new AuthorizePlayerViewModel()
            {
                PlayerId = human.Id,
                Name = human.Name,
                ResumeGame = resumeGame
            };

            return authorizePlayerViewModel;
        }

        public async Task<long> CreateGame(long playerId, int amountOfBots)
        {
            var game = new Game();
            game.Id = await _gameRepository.Create(game);

            List<Player> players = await CreatePlayerList(playerId, amountOfBots);
            var gamePlayers = new List<GamePlayer>();
            foreach (Player player in players)
            {
                var gamePlayer = new GamePlayer()
                {
                    GameId = game.Id,
                    PlayerId = player.Id,
                    Score = GameValueHelper.DefaultPlayerScore,
                    BetPayCoefficient = BetValueHelper.DefaultCoefficient,
                    Bet = GameValueHelper.Zero,
                    RoundScore = GameValueHelper.Zero,
                    Player = player
                };

                gamePlayers.Add(gamePlayer);
            }

            await _gamePlayerRepository.CreateMany(gamePlayers, ToStringHelper.GetTableName(typeof(GamePlayer)));
            List<Log> logs = LogHelper.GetCreationGameLogs(gamePlayers, game);
            await _logRepository.CreateMany(logs, ToStringHelper.GetTableName(typeof(Log)));

            long gameId = game.Id;
            return gameId;
        }

        public async Task<long> ResumeGame(long playerId)
        {
            long gameId = await _gameRepository.GetIdByPlayerId(playerId);
            return gameId;
        }

        public async Task<InitRoundViewModel> InitRound(long gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            List<GamePlayer> players = (await _gamePlayerRepository.GetAllForInitRound(gameId)).ToList();
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            string isGameOver = IsGameOver(human, dealer);
            InitRoundViewModel initRoundViewModel = CustomMapper.GetInitRoundViewModel(game, players, isGameOver);
            return initRoundViewModel;
        }
        
        private Player CreatePlayer(string name, PlayerType playerType)
        {
            var player = new Player();
            player.Name = name;
            player.Type = (int)playerType;
            return player;
        }

        private async Task<List<Player>> CreatePlayerList(long playerId, int amountOfBots)
        {
            var players = new List<Player>();
            var random = new Random();
            Player dealer = CreatePlayer(((BotName)random.Next(GameValueHelper.BotNameAmount)).ToString(), PlayerType.Dealer);
            players.Add(dealer);

            for (int i = 0; i < amountOfBots; i++)
            {
                Player bot = CreatePlayer(((BotName)random.Next(GameValueHelper.BotNameAmount)).ToString(), PlayerType.Bot);
                players.Add(bot);
            }

            players = await _playerRepository.CreateMany(players);
            players.Add(await _playerRepository.Get(playerId));
            return players;
        }

        private string IsGameOver(GamePlayer human, GamePlayer dealer)
        {
            string isGameOver = string.Empty;

            if (dealer.Score <= GameValueHelper.Zero)
            {
                isGameOver = GameMessageHelper.DealerIsLoser;
            }

            if (human.Score <= GameValueHelper.Zero)
            {
                isGameOver = GameMessageHelper.DealerIsWinner;
            }

            return isGameOver;
        }
    }
}