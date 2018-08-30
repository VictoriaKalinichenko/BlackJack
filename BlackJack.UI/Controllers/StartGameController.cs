using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
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
            return View();
        }
        
        public ActionResult StartGame()
        {
            try
            {
                NameAndBotAmountInputViewModel startInputViewModel = new NameAndBotAmountInputViewModel();
                return View(startInputViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "Game", new { message = message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> StartGame(NameAndBotAmountInputViewModel startInputViewModel)
        {
            try
            {
                string validationString = await _startGameService.PlayerNameValidation(startInputViewModel.HumanName);

                if (String.IsNullOrEmpty(validationString))
                {
                    int gameId = await _startGameService.CreateGame(startInputViewModel);
                    return RedirectToAction("RoundStart", "Game", new { gameId = gameId });
                }

                ModelState.AddModelError("HumanName", validationString);

                return View(startInputViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "Game", new { message = ex.Message });
            }
        }
    }
}