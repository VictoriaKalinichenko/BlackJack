using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.GameHistory;
using Newtonsoft.Json;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class GameHistoryController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameHistoryService _gameHistoryService;

        public GameHistoryController(IGameHistoryService gameHistoryService)
        {
            _gameHistoryService = gameHistoryService;
        }

        public ActionResult Index(string userName)
        {
            return View(model: userName);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                GetGameHistoryView getGameHistoryView = await _gameHistoryService.Get();
                string jsonResult = JsonConvert.SerializeObject(getGameHistoryView);
                return Content(jsonResult, "application/json");
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return RedirectToAction("Error", "Start", new { message = GameMessage.HistoryMessagesError});
            }
        }
    }
}