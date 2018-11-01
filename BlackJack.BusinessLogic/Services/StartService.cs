using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.Start;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class StartService : IStartService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IHistoryMessageManager _historyMessageManager;
        

        public StartService(IGameRepository gameRepository, IPlayerRepository playerRepository, 
            IGamePlayerRepository gamePlayerRepository, IHistoryMessageManager historyMessageManager)
        {
            _gamePlayerRepository = gamePlayerRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _historyMessageManager = historyMessageManager;
        }

        public async Task CreatePlayer(string name)
        {
            Player human = await _playerRepository.SelectByName(name, PlayerType.Human);
            if (human == null)
            {
                human = CustomMapper.GetPlayer(name, PlayerType.Human);
                await _playerRepository.Create(human);
            }
        }

        public async Task<AuthorizePlayerStartView> AuthorizePlayer(string name)
        {
            Player human = await _playerRepository.SelectByName(name, PlayerType.Human);
            Game game = await _gameRepository.GetByPlayerId(human.Id);
            AuthorizePlayerStartView authorizePlayerStartView = CustomMapper.GetAuthorizePlayerStartView(human, game);
            return authorizePlayerStartView;
        }

        public async Task<long> CreateGame(long playerId, int amountOfBots)
        {
            var game = new Game();
            game.Id = await _gameRepository.Create(game);

            List<Player> players = await CreatePlayerList(playerId, amountOfBots);
            var gamePlayers = new List<GamePlayer>();
            foreach (Player player in players)
            {
                GamePlayer gamePlayer = CustomMapper.GetGamePlayer(player, game.Id);
                gamePlayers.Add(gamePlayer);
            }

            await _gamePlayerRepository.CreateMany(gamePlayers);
            await _historyMessageManager.AddMessagesForCreateGame(gamePlayers, game);
            long gameId = game.Id;
            return gameId;
        }

        public async Task<long> ResumeGame(long playerId)
        {
            long gameId = await _gameRepository.GetIdByPlayerId(playerId);
            return gameId;
        }

        public async Task<InitializeStartView> InitializeRound(long gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            string userName = await _gameRepository.GetHumanNameByGameId(gameId);
            InitializeStartView initializeStartView = CustomMapper.GetInitializeStartView(game, userName);
            return initializeStartView;
        }
                
        private async Task<List<Player>> CreatePlayerList(long playerId, int amountOfBots)
        {
            var players = new List<Player>();
            Player dealer = CustomMapper.GetPlayer(GameStrings.DealerName, PlayerType.Dealer);
            players.Add(dealer);

            for (int i = 0; i < amountOfBots; i++)
            {
                Player bot = CustomMapper.GetPlayer(GameStrings.BotName + i, PlayerType.Bot);
                players.Add(bot);
            }

            players = await _playerRepository.CreateMany(players);
            players.Add(await _playerRepository.Get(playerId));
            return players;
        }
    }
}