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
            ValidateNameViewModel validateNameViewModel = new ValidateNameViewModel();
            return View(validateNameViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateName(ValidateNameViewModel validateNameViewModel)
        {
            try
            {
                validateNameViewModel.ValidationMessage = _startService.ValidateName(validateNameViewModel.UserName);

                if(string.IsNullOrEmpty(validateNameViewModel.ValidationMessage))
                {
                    await _startService.CreatePlayer(validateNameViewModel.UserName);
                    return RedirectToAction("AuthorizePlayer", new { userName = validateNameViewModel.UserName });
                }
                
                return View(validateNameViewModel);
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
                AuthorizePlayerViewModel authorizePlayerViewModel = await _startService.AuthorizePlayer(userName);
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
                InitRoundViewModel initRoundViewModel = await _startService.InitRound(gameId);
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