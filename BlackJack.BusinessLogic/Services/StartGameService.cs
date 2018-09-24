using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.ViewModels;
using BlackJack.ViewModels.Enums;
using AutoMapper;

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

        public async Task<StartGameAuthorizePlayerView> AuthorizePlayer(string name)
        {
            Player human = await _playerRepository.SelectByName(name, (int)PlayerType.Human);

            bool resumeGame = true;
            Game game = await _gameRepository.GetByPlayerId(human.Id);
            if (game == null || !string.IsNullOrEmpty(game.Result))
            {
                resumeGame = false;
            }

            var startGameAuthorizePlayerView = new StartGameAuthorizePlayerView()
            {
                PlayerId = human.Id,
                Name = human.Name,
                ResumeGame = resumeGame
            };

            return startGameAuthorizePlayerView;
        }

        public async Task<int> CreateGame(int playerId, int amountOfBots)
        {
            var logs = new List<Log>();
            Game game = await _gameRepository.Create();
            logs.Add(new Log() { DateTime = DateTime.Now, GameId = game.Id, Message = LogMessageHelper.GameCreated(game.Id, game.Stage) });

            List<Player> players = await CreatePlayerList(playerId, amountOfBots);
            var gamePlayers = new List<GamePlayer>();
            foreach (Player player in players)
            {
                var gamePlayer = new GamePlayer()
                {
                    GameId = game.Id,
                    PlayerId = player.Id,
                    Score = GameValueHelper.DefaultPlayerScore,
                    BetPayCoefficient = BetValueHelper.BetDefaultCoefficient,
                    Bet = BetValueHelper.BetZero,
                    RoundScore = GameValueHelper.ZeroScore,
                };

                gamePlayers.Add(gamePlayer);
                logs.Add(new Log() { DateTime = DateTime.Now, GameId = game.Id, Message = LogMessageHelper.PlayerAddedToGame(((PlayerType)player.Type).ToString(), player.Id, player.Name, gamePlayer.Score) });
            }

            await _gamePlayerRepository.CreateMany(gamePlayers);
            await _logRepository.CreateMany(logs);

            int gameId = game.Id;
            return gameId;
        }

        public async Task<int> ResumeGame(int playerId)
        {
            int gameId = await _gameRepository.GetIdByPlayerId(playerId);
            return gameId;
        }

        public async Task<StartGameStartRoundView> GetStartGameStartRoundView(int gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            StartGameStartRoundView startGameStartRoundView = Mapper.Map<Game, StartGameStartRoundView>(game);

            GamePlayer dealer = await _gamePlayerRepository.GetSpecificPlayerForStartRound(gameId, (int)PlayerType.Dealer);
            GamePlayer human = await _gamePlayerRepository.GetSpecificPlayerForStartRound(gameId, (int)PlayerType.Human);
            IEnumerable<GamePlayer> bots = await _gamePlayerRepository.GetSpecificPlayersForStartRound(gameId, (int)PlayerType.Bot);

            startGameStartRoundView.Dealer = Mapper.Map<GamePlayer, PlayerStartGameStartRoundItem>(dealer);
            startGameStartRoundView.Human = Mapper.Map<GamePlayer, PlayerStartGameStartRoundItem>(human);
            startGameStartRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<PlayerStartGameStartRoundItem>>(bots);
            
            startGameStartRoundView.IsGameOver = IsGameOver(human, dealer);
            return startGameStartRoundView;
        }
        
        private Player CreatePlayer(string name, PlayerType playerType)
        {
            var player = new Player();
            player.Name = name;
            player.Type = (int)playerType;
            return player;
        }

        private async Task<List<Player>> CreatePlayerList(int playerId, int amountOfBots)
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

            if (dealer.Score <= GameValueHelper.ZeroScore)
            {
                isGameOver = GameMessageHelper.DealerIsLoser;
            }

            if (human.Score <= GameValueHelper.ZeroScore)
            {
                isGameOver = GameMessageHelper.DealerIsWinner;
            }

            return isGameOver;
        }
    }
}