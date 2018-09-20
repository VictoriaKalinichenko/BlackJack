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
        
        public ActionResult ValidateName()
        {
            StartGameValidateNameView startGameValidateNameView = new StartGameValidateNameView();
            return View(startGameValidateNameView);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateName(StartGameValidateNameView startGameValidateNameView)
        {
            try
            {
                startGameValidateNameView.ValidationMessage = _startGameService.ValidatePlayerName(startGameValidateNameView.UserName);

                if(string.IsNullOrEmpty(startGameValidateNameView.ValidationMessage))
                {
                    await _startGameService.CreatePlayer(startGameValidateNameView.UserName);
                    return RedirectToAction("AuthorizePlayer", new { userName = startGameValidateNameView.UserName });
                }
                
                return View(startGameValidateNameView);
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
                StartGameAuthorizePlayerView startGameAuthorizePlayerView = await _startGameService.AuthorizePlayer(userName);
                return View(startGameAuthorizePlayerView);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.PlayerAuthError });
            }
        }

        [HttpPost]
        public async Task<ActionResult> StartNewGame(int playerId, int amountOfBots)
        {
            try
            {
                int gameId = await _startGameService.CreateGame(playerId, amountOfBots);
                return RedirectToAction("StartRound", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.GameCreationError });
            }
        }

        public async Task<ActionResult> ResumeGame(int playerId)
        {
            try
            {
                int gameId = await _startGameService.ResumeGame(playerId);
                return RedirectToAction("StartRound", new { gameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessageHelper.GameResumingError });
            }
        }

        public async Task<ActionResult> StartRound(int gameId)
        {
            try
            {
                StartGameStartRoundView game = await _startGameService.GetStartGameStartRoundView(gameId);
                return View(game);
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