using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class StartGameController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStartGameService _startGameService;


        public StartGameController (IStartGameService startGameService)
        {
            _startGameService = startGameService;
        }
        
        public ActionResult Index()
        {
            NameValidationViewModel nameValidationViewModel = new NameValidationViewModel();
            return View(nameValidationViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(NameValidationViewModel nameValidationViewModel)
        {
            try
            {
                nameValidationViewModel.ValidateMessage = _startGameService.PlayerNameValidation(nameValidationViewModel.UserName);

                if(string.IsNullOrEmpty(nameValidationViewModel.ValidateMessage))
                {
                    string userName = await _startGameService.PlayerCreation(nameValidationViewModel.UserName);
                    return RedirectToAction("MainPage", new { userName = userName });
                }
                
                return View(nameValidationViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.PlayerAuthError;
                return RedirectToAction("Error", new { message = message });
            }
        }
        
        public async Task<ActionResult> MainPage(string userName)
        {
            try
            {
                AuthPlayerViewModel authPlayerViewModel = await _startGameService.PlayerAuthorization(userName);
                return View(authPlayerViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.PlayerAuthError;
                return RedirectToAction("Error", new { message = message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> StartNewGame(int playerId, int amountOfBots)
        {
            try
            {
                int gameId = await _startGameService.CreateGame(playerId, amountOfBots);
                return RedirectToAction("Round", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameCreationError;
                return RedirectToAction("Error", new { message = message });
            }
        }

        public async Task<ActionResult> ResumeGame(int playerId)
        {
            try
            {
                int gameId = await _startGameService.ResumeGame(playerId);
                return RedirectToAction("Round", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameResumingError;
                return RedirectToAction("Error", new { message = message });
            }
        }

        public async Task<ActionResult> Round(int gameId)
        {
            try
            {
                GameViewModel game = await _startGameService.GetGame(gameId);
                return View(game);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameLoadingError;
                return RedirectToAction("Error", new { message = message });
            }
        }

        public ActionResult Error(string message)
        {
            return View((object)message);
        }
    }
}