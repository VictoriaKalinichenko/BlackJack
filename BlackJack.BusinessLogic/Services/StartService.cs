using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels.Start;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class StartService : IStartService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly ILogRepository _logRepository;
        

        public StartService(IGameRepository gameRepository, IPlayerRepository playerRepository, IGamePlayerRepository gamePlayerRepository, ILogRepository logRepository)
        {
            _gamePlayerRepository = gamePlayerRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _logRepository = logRepository;
        }

        public string ValidatePlayerName(string name)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(name))
            {
                result = GameMessageHelper.NameFieldIsEmpty;
            }
            return result;
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
            var logs = new List<Log>();
            Game game = await _gameRepository.Create();
            logs.Add(new Log() { GameId = game.Id, Message = LogMessageHelper.GameCreated(game.Id, game.Stage) });

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
                };

                gamePlayers.Add(gamePlayer);
                logs.Add(new Log() { GameId = game.Id, Message = LogMessageHelper.PlayerAddedToGame(((PlayerType)player.Type).ToString(), player.Id, player.Name, gamePlayer.Score) });
            }

            await _gamePlayerRepository.CreateMany(gamePlayers);
            await _logRepository.CreateMany(logs);

            long gameId = game.Id;
            return gameId;
        }

        public async Task<long> ResumeGame(long playerId)
        {
            long gameId = await _gameRepository.GetIdByPlayerId(playerId);
            return gameId;
        }

        public async Task<BeginRoundViewModel> GetBeginRoundViewModel(long gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            BeginRoundViewModel beginRoundViewModel = Mapper.Map<Game, BeginRoundViewModel>(game);

            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerForStartRound(gameId, (int)PlayerType.Dealer);
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerForStartRound(gameId, (int)PlayerType.Human);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersForStartRound(gameId, (int)PlayerType.Bot);

            beginRoundViewModel.Dealer = Mapper.Map<GamePlayer, PlayerBeginRoundViewItem>(dealer);
            beginRoundViewModel.Human = Mapper.Map<GamePlayer, PlayerBeginRoundViewItem>(human);
            beginRoundViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<PlayerBeginRoundViewItem>>(bots);
            
            beginRoundViewModel.IsGameOver = IsGameOver(human, dealer);
            return beginRoundViewModel;
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