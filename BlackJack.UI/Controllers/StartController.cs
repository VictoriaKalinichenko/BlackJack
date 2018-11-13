using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Services.Interfaces;
using BlackJack.ViewModels.Start;
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
        
        public ActionResult SearchGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchGame(string userName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(userName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                SearchGameStartView view = await _startService.SearchGame(userName);

                if (view.IsGameExist)
                {
                    return RedirectToAction("Initialize", new { view.GameId });
                }

                return RedirectToAction("CreateGame", new { userName });
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return RedirectToAction("Display", "Error", new { message = GameMessage.PlayerAuthorizationError });
            }
        }
        
        public ActionResult CreateGame(string userName)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(userName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                var view = new CreateGameStartView();
                view.UserName = userName;
                return View(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return RedirectToAction("Display", "Error", new { message = GameMessage.GameCreationError });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateGame(CreateGameStartView view)
        {
            try
            {
                if (view == null || 
                    String.IsNullOrWhiteSpace(view.UserName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                long gameId = await _startService.CreateGame(view);
                return RedirectToAction("Initialize", new { gameId = gameId, isNewGame = true });
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return RedirectToAction("Display", "Error", new { message = GameMessage.GameCreationError });
            }
        }
        
        public ActionResult Initialize(long gameId, bool isNewGame = false)
        {
            try
            {
                var view = new InitializeStartView();
                view.GameId = gameId;
                view.IsNewGame = isNewGame;
                return View(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return RedirectToAction("Display", "Error", new { message = GameMessage.GameLoadingError });
            }
        }
    }
}