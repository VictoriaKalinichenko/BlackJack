using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Services.Interfaces;
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                GetGameHistoryView view = await _gameHistoryService.Get();
                string jsonResult = JsonConvert.SerializeObject(view);
                return Content(jsonResult, "application/json");
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return RedirectToAction("Display", "Error", new { message = GameMessage.HistoryMessagesError});
            }
        }
    }
}