using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
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
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string userName)
        {
            try
            {
                await _startService.CreatePlayer(userName);
                return RedirectToAction("AuthorizePlayer", new { userName = userName });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessage.PlayerAuthError });
            }
        }
        
        public async Task<ActionResult> AuthorizePlayer(string userName)
        {
            try
            {
                if(String.IsNullOrEmpty(userName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                AuthorizePlayerStartView authorizePlayerStartView = await _startService.AuthorizePlayer(userName);
                return View(authorizePlayerStartView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessage.PlayerAuthError });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateGame(long playerId, int amountOfBots)
        {
            try
            {
                long gameId = await _startService.CreateGame(playerId, amountOfBots);
                return RedirectToAction("Initialize", new { gameId = gameId });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessage.GameCreationError });
            }
        }

        public async Task<ActionResult> ResumeGame(long playerId)
        {
            try
            {
                long gameId = await _startService.ResumeGame(playerId);
                return RedirectToAction("Initialize", new { gameId = gameId });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessage.GameResumingError });
            }
        }

        public async Task<ActionResult> Initialize(long gameId)
        {
            try
            {
                InitializeStartView initializeStartView = await _startService.InitializeRound(gameId);
                return View(initializeStartView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", new { message = GameMessage.GameLoadingError });
            }
        }

        public ActionResult Error(string message)
        {
            return View((object)message);
        }
    }
}