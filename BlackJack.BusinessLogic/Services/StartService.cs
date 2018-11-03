using AutoMapper;
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

        public async Task<IndexStartView> SearchGameForPlayer(string name)
        {
            Game game = await _gameRepository.GetByHumanName(name);
            IndexStartView indexStartView = new IndexStartView() { IsGameExist = false };

            if (game != null)
            {
                indexStartView.IsGameExist = true;
                indexStartView.GameId = game.Id;
            }

            return indexStartView;
        }
        
        public async Task<long> CreateGame(CreateGameStartView createGameStartView)
        {
            var game = new Game();
            game.Id = await _gameRepository.Create(game);

            List<Player> players = await CreatePlayerList(createGameStartView.UserName, createGameStartView.AmountOfBots);
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
        
        public async Task<InitializeStartView> InitializeRound(long gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            InitializeStartView initializeStartView = Mapper.Map<Game, InitializeStartView>(game);
            return initializeStartView;
        }
                
        private async Task<List<Player>> CreatePlayerList(string humanName, int amountOfBots)
        {
            var players = new List<Player>();
            Player human = CustomMapper.GetPlayer(humanName, PlayerType.Human);
            players.Add(human);

            Player dealer = CustomMapper.GetPlayer(PlayerName.DealerName, PlayerType.Dealer);
            players.Add(dealer);

            for (int i = 0; i < amountOfBots; i++)
            {
                Player bot = CustomMapper.GetPlayer(PlayerName.BotName + i, PlayerType.Bot);
                players.Add(bot);
            }

            players = await _playerRepository.CreateMany(players);
            return players;
        }
    }
}