using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class ApiController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }
        
        public async Task<ActionResult> Index(int gameId)
        {
            try
            {
                GameViewModel game = await _apiService.GetGame(gameId);
                return View(game);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "CardAndCheck", new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPlayer(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _apiService.GetGamePlayer(gamePlayerId);
                string message = GameMessage.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDealerInFirstPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _apiService.GetDealerInFirstPhase(gamePlayerId);
                string message = GameMessage.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDealerInSecondPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayer = await _apiService.GetDealerInSecondPhase(gamePlayerId);
                string message = GameMessage.Success;
                return Json(new { GamePlayer = gamePlayer, Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> BetsCreation(int bet, int humanGamePlayerId, int inGameId)
        {
            try
            {
                string message = await _apiService.BetValidation(bet, humanGamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    await _apiService.BetsCreation(bet, inGameId);
                    message = GameMessage.Success;
                }

                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> HumanRoundResult(int inGameId)
        {
            try
            {
                string roundResult = await _apiService.HumanRoundResult(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message, HumanRoundResult = roundResult }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateGamePlayersForNewRound(int inGameId)
        {
            try
            {
                await _apiService.BetPayments(inGameId);
                await _apiService.UpdateGamePlayersForNewRound(inGameId);
                await _apiService.BotsRemoving(inGameId);
                string isGameOver = await _apiService.IsGameOver(inGameId);

                if (!string.IsNullOrEmpty(isGameOver))
                {
                    await _apiService.OnGameOver(inGameId, isGameOver);
                }

                string message = GameMessage.Success;
                return Json(new { Message = message, IsGameOver = isGameOver }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}