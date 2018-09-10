using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    [RoutePrefix("PlayerLogic")]
    public class PlayerLogicController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IPlayerLogicService _playerLogicService;

        public PlayerLogicController(IPlayerLogicService playerLogicService)
        {
            _playerLogicService = playerLogicService;
        }
        
        [Route("GetPlayer"), HttpGet]
        public async Task<IHttpActionResult> GetPlayer(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetGamePlayer(gamePlayerId);
                return Json(new { GamePlayer = gamePlayer, Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("GetDealerInFirstPhase"), HttpGet]
        public async Task<IHttpActionResult> GetDealerInFirstPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetDealerInFirstPhase(gamePlayerId);
                return Json(new { GamePlayer = gamePlayer, Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("GetDealerInSecondPhase"), HttpGet]
        public async Task<IHttpActionResult> GetDealerInSecondPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _playerLogicService.GetDealerInSecondPhase(gamePlayerId);
                return Json(new { GamePlayer = gamePlayer, Message = GameMessageHelper.Success });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }

        [Route("BetsCreation"), HttpPost]
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
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("HumanRoundResult"), HttpGet]
        public async Task<IHttpActionResult> HumanRoundResult(int inGameId)
        {
            try
            {
                string roundResult = await _playerLogicService.HumanRoundResult(inGameId);
                return Json(new { Message = GameMessageHelper.Success, HumanRoundResult = roundResult });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
        
        [Route("UpdateGamePlayersForNewRound"), HttpGet]
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

                return Json(new { Message = GameMessageHelper.Success, IsGameOver = isGameOver });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = GameMessageHelper.GameError });
            }
        }
    }
}