using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.BusinessLogic.Services
{
    public class PlayerLogicService : IPlayerLogicService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IGameRepository _gameRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ILogRepository _logRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;


        public PlayerLogicService(IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository,
            IGamePlayerProvider gamePlayerProvider, IPlayerCardProvider playerCardProvider, ICardCheckProvider cardCheckProvider, ILogRepository logRepository)
        {
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _logRepository = logRepository;

            _gamePlayerProvider = gamePlayerProvider;
            _playerCardProvider = playerCardProvider;
            _cardCheckProvider = cardCheckProvider;
        }

        public async Task<GamePlayerViewModel> GetGamePlayer(int gamePlayerId)
        {
            GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
            GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
            gamePlayerViewModel.Id = gamePlayer.Id;
            gamePlayerViewModel.Bet = gamePlayer.Bet;
            gamePlayerViewModel.Score = gamePlayer.Score;
            gamePlayerViewModel.CardScore = gamePlayer.RoundScore;
            List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
            gamePlayerViewModel.Cards = _playerCardProvider.GetCardsStringList(playerCards);
            return gamePlayerViewModel;
        }

        public async Task<GamePlayerViewModel> GetDealerInFirstPhase(int gamePlayerId)
        {
            GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
            GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
            gamePlayerViewModel.Id = gamePlayer.Id;
            gamePlayerViewModel.Score = gamePlayer.Score;
            List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
            gamePlayerViewModel.Cards = new List<string>();
            gamePlayerViewModel.Cards.Add(_playerCardProvider.ConvertCardToString(playerCards[0].Card));
            return gamePlayerViewModel;
        }

        public async Task<GamePlayerViewModel> GetDealerInSecondPhase(int gamePlayerId)
        {
            GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
            GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
            gamePlayerViewModel.Id = gamePlayer.Id;
            gamePlayerViewModel.Score = gamePlayer.Score;
            gamePlayerViewModel.CardScore = gamePlayer.RoundScore;
            List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
            gamePlayerViewModel.Cards = _playerCardProvider.GetCardsStringList(playerCards);
            return gamePlayerViewModel;
        }

        public async Task<string> BetValidation(int bet, int gamePlayerId)
        {
            string message = string.Empty;
            GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);

            if (bet > gamePlayer.Score)
            {
                message = GameMessageHelper.BetMoreThanScore;
            }

            if (bet <= BetValueHelper.BetZero)
            {
                message = GameMessageHelper.BetLessThanMin;
            }

            return message;
        }

        public async Task<int> BetsCreation(int bet, int inGameId)
        {
            int outGameId = 0;

            Game game = await _gameRepository.Get(inGameId);
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(game.Id);
            await _gamePlayerProvider.BetsCreation(gamePlayers, bet);

            outGameId = game.Id;
            game.Stage = StageHelper.RoundStart;
            await _gameRepository.Update(game);
            await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);

            return outGameId;
        }

        public async Task BetPayments(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            await _gamePlayerProvider.RoundBetPayments(gamePlayers);
        }

        public async Task<string> HumanRoundResult(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
            GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();

            string humanRoundResult;
            humanRoundResult = _cardCheckProvider.HumanRoundResult(human.BetPayCoefficient);
            return humanRoundResult;
        }

        public async Task UpdateGamePlayersForNewRound(int gameId)
        {
            Game game = await _gameRepository.Get(gameId);
            game.Stage = StageHelper.RoundStart;
            await _gameRepository.Update(game);

            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.RoundScore = CardValueHelper.CardZeroScore;
                await _gamePlayerRepository.Update(gamePlayer);
                await _playerCardRepository.DeleteByGamePlayerId(gamePlayer.Id);
            }
        }

        public async Task BotsRemoving(int gameId)
        {
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (!gamePlayer.Player.IsDealer && !gamePlayer.Player.IsHuman && IsZeroScore(gamePlayer))
                {
                    await _gamePlayerRepository.Delete(gamePlayer.Id);
                }
            }
        }

        public async Task<string> IsGameOver(int gameId)
        {
            string isGameOver = string.Empty;
            IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.Player.IsDealer && IsZeroScore(gamePlayer))
                {
                    isGameOver = GameMessageHelper.DealerIsLoser;
                }

                if (gamePlayer.Player.IsHuman && IsZeroScore(gamePlayer))
                {
                    isGameOver = GameMessageHelper.DealerIsWinner;
                }
            }

            return isGameOver;
        }
        
        public async Task OnGameOver(int gameId, string gameResult)
        {
            Game game = await _gameRepository.Get(gameId);
            game.Result = gameResult;
            await _gameRepository.Update(game);
        }

        private bool IsZeroScore(GamePlayer gamePlayer)
        {
            bool result = false;

            if (gamePlayer.Score <= GameValueHelper.ZeroScore)
            {
                result = true;
            }

            return result;
        }
    }
}