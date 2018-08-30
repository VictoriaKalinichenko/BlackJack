using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class GameController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameService _gameService;

        public GameController (IGameService gameService)
        {
            _gameService = gameService;
        }

        public ActionResult Error(string message)
        {
            return View((object)message);
        }
        
        public async Task<ActionResult> RoundStart(int gameId)
        {
            try
            {
                BetInputViewModel betInputViewModel = new BetInputViewModel();
                betInputViewModel.Game = await _gameService.GenerateGameStartViewModel(gameId);
                betInputViewModel.HumanBet = BetValue.BetGenCoef;
                
                return View(betInputViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "Game", new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> RoundStart(BetInputViewModel betInputViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int gameId = await _gameService.BetsCreation(betInputViewModel);
                    return RedirectToAction("CardDistribution", new { gameId = gameId });
                }

                return View(betInputViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "Game", new { message = ex.Message });
            }
        }

        public async Task<ActionResult> CardDistribution(int gameId)
        {
            try
            {
                bool humanBjAndDealerBjDanger = await _gameService.RoundFirstPhase(gameId);
                GameViewModel gameViewModel = await _gameService.GenerateFirstPhaseGameViewModel(gameId);
                return View(gameViewModel);
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