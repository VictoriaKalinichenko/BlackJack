using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class PlayerLogicController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IPlayerLogicService _playerLogicService;

        public PlayerLogicController(IPlayerLogicService playerLogicService)
        {
            _playerLogicService = playerLogicService;
        }
        
        [Route("PlayerLogic/GetPlayer"), HttpGet]
        public async Task<IHttpActionResult> GetPlayer(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetGamePlayer(gamePlayerId);
                string message = GameMessageHelper.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("PlayerLogic/GetDealerInFirstPhase"), HttpGet]
        public async Task<IHttpActionResult> GetDealerInFirstPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetDealerInFirstPhase(gamePlayerId);
                string message = GameMessageHelper.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("PlayerLogic/GetDealerInSecondPhase"), HttpGet]
        public async Task<IHttpActionResult> GetDealerInSecondPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetDealerInSecondPhase(gamePlayerId);
                string message = GameMessageHelper.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }

        [Route("PlayerLogic/BetsCreation"), HttpPost]
        public async Task<IHttpActionResult> BetsCreation(BetCreationApiViewModel viewModel)
        {
            try
            {
                string message = await _playerLogicService.BetValidation(viewModel.Bet, viewModel.HumanGamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    await _playerLogicService.BetsCreation(viewModel.Bet, viewModel.InGameId);
                    message = GameMessageHelper.Success;
                }

                return Json(new { Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("PlayerLogic/HumanRoundResult"), HttpGet]
        public async Task<IHttpActionResult> HumanRoundResult(int inGameId)
        {
            try
            {
                string roundResult = await _playerLogicService.HumanRoundResult(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message, HumanRoundResult = roundResult });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("PlayerLogic/UpdateGamePlayersForNewRound"), HttpGet]
        public async Task<IHttpActionResult> UpdateGamePlayersForNewRound(int inGameId)
        {
            try
            {
                await _playerLogicService.BetPayments(inGameId);
                await _playerLogicService.UpdateGamePlayersForNewRound(inGameId);
                await _playerLogicService.BotsRemoving(inGameId);
                string isGameOver = await _playerLogicService.IsGameOver(inGameId);

                if (!string.IsNullOrEmpty(isGameOver))
                {
                    await _playerLogicService.OnGameOver(inGameId, isGameOver);
                }

                string message = GameMessageHelper.Success;
                return Json(new { Message = message, IsGameOver = isGameOver });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
    }
}