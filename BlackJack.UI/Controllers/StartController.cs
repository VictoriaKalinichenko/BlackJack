using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels.Start;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class StartController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStartService _startService;


        public StartController (IStartService startService)
        {
            _startService = startService;
        }
        
        public ActionResult ValidateName()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidateName(string userName)
        {
            try
            {
                await _startService.CreatePlayer(userName);
                return RedirectToAction("AuthorizePlayer", new { userName = userName });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.PlayerAuthError });
            }
        }
        
        public async Task<ActionResult> AuthorizePlayer(string userName)
        {
            try
            {
                if(String.IsNullOrEmpty(userName))
                {
                    new Exception(GameMessageHelper.ReceivedDataError);
                }

                StartAuthorizePlayerViewModel authorizePlayerViewModel = await _startService.AuthorizePlayer(userName);
                return View(authorizePlayerViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.PlayerAuthError });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateGame(long playerId, int amountOfBots)
        {
            try
            {
                long gameId = await _startService.CreateGame(playerId, amountOfBots);
                return RedirectToAction("InitRound", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.GameCreationError });
            }
        }

        public async Task<ActionResult> ResumeGame(long playerId)
        {
            try
            {
                long gameId = await _startService.ResumeGame(playerId);
                return RedirectToAction("InitRound", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.GameResumingError });
            }
        }

        public async Task<ActionResult> InitRound(long gameId)
        {
            try
            {
                StartInitRoundViewModel initRoundViewModel = await _startService.InitRound(gameId);
                return View(initRoundViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.GameLoadingError });
            }
        }

        public ActionResult Error(string message)
        {
            return View((object)message);
        }
    }
}