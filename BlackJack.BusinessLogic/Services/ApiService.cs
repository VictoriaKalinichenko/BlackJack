using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.BusinessLogic.Services
{
    public class ApiService : IApiService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IGameRepository _gameRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ILogRepository _logRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;


        public ApiService(IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository,
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
            try
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
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayerViewModel> GetDealerInFirstPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
                GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
                gamePlayerViewModel.Id = gamePlayer.Id;
                gamePlayerViewModel.Score = gamePlayer.Score;
                List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                gamePlayerViewModel.Cards = new List<string>();
                gamePlayerViewModel.Cards.Add(InitialDeck.Cards.Where(m => m.Id == playerCards[0].CardId).First().ToString());
                return gamePlayerViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayerViewModel> GetDealerInSecondPhase(int gamePlayerId)
        {
            try
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
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GameViewModel> GetGame(int gameId)
        {
            try
            {
                GameViewModel gameViewModel = new GameViewModel();
                gameViewModel.Bots = new List<BotViewModel>();
                gameViewModel.Id = gameId;
                Game game = await _gameRepository.Get(gameId);
                gameViewModel.Stage = game.Stage;

                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                int botCount = 0;

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    PlayerViewModel playerViewModel = new PlayerViewModel();

                    if (gamePlayer.Player.IsDealer || gamePlayer.Player.IsHuman)
                    {
                        playerViewModel.Id = gamePlayer.Id;
                        playerViewModel.Name = gamePlayer.Player.Name;
                        playerViewModel.Score = gamePlayer.Score;
                    }

                    if (gamePlayer.Player.IsDealer)
                    {
                        gameViewModel.Dealer = playerViewModel;
                    }

                    if (gamePlayer.Player.IsHuman)
                    {
                        gameViewModel.Human = playerViewModel;
                        gameViewModel.Name = playerViewModel.Name;
                    }

                    if (!gamePlayer.Player.IsHuman && !gamePlayer.Player.IsDealer)
                    {
                        BotViewModel botViewModel = new BotViewModel();
                        botViewModel.Id = gamePlayer.Id;
                        botViewModel.Name = gamePlayer.Player.Name;
                        botViewModel.Score = gamePlayer.Score;
                        botViewModel.GamePlayerDomId = DomId.BotGamePlayerId + (++botCount).ToString();
                        botViewModel.GamePlayDomId = DomId.BotGamePlayId + botCount.ToString();
                        gameViewModel.Bots.Add(botViewModel);
                    }
                }

                return gameViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<string> BetValidation(int bet, int gamePlayerId)
        {
            try
            {
                string message = string.Empty;
                GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);

                if (bet > gamePlayer.Score)
                {
                    message = GameMessage.BetMoreThanScore;
                }

                if (bet <= BetValue.BetZero)
                {
                    message = GameMessage.BetLessThanMin;
                }

                return message;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
        
        public async Task<int> BetsCreation(int bet, int inGameId)
        {
            try
            {
                int outGameId = 0;

                Game game = await _gameRepository.Get(inGameId);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(game.Id);
                await _gamePlayerProvider.BetsCreation(gamePlayers, bet);

                outGameId = game.Id;
                game.Stage = Stage.RoundStart;
                await _gameRepository.Update(game);
                await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);

                return outGameId;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
        
        public async Task BetPayments(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                await _gamePlayerProvider.RoundBetPayments(gamePlayers);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<string> HumanRoundResult(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();

                string humanRoundResult;
                humanRoundResult = _cardCheckProvider.HumanRoundResult(human.BetPayCoefficient);
                return humanRoundResult;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task UpdateGamePlayersForNewRound(int gameId)
        {
            try
            {
                Game game = await _gameRepository.Get(gameId);
                game.Stage = Stage.RoundStart;
                await _gameRepository.Update(game);

                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                
                foreach(GamePlayer gamePlayer in gamePlayers)
                {
                    gamePlayer.RoundScore = CardValue.CardZeroScore;
                    await _gamePlayerRepository.Update(gamePlayer);
                    await _playerCardRepository.DeleteByGamePlayerId(gamePlayer.Id);
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task BotsRemoving(int gameId)
        {
            try
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
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<string> IsGameOver(int gameId)
        {
            try
            {
                string isGameOver = string.Empty;
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    if (gamePlayer.Player.IsDealer && IsZeroScore(gamePlayer))
                    {
                        isGameOver = GameMessage.DealerIsLoser;
                    }

                    if (gamePlayer.Player.IsHuman && IsZeroScore(gamePlayer))
                    {
                        isGameOver = GameMessage.DealerIsWinner;
                    }
                }

                return isGameOver;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task OnGameOver(int gameId, string gameResult)
        {
            try
            {
                Game game = await _gameRepository.Get(gameId);
                game.Result = gameResult;
                await _gameRepository.Update(game);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private bool IsZeroScore(GamePlayer gamePlayer)
        {
            try
            {
                bool result = false;

                if (gamePlayer.Score <= GameValue.ZeroScore)
                {
                    result = true;
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
    }
}