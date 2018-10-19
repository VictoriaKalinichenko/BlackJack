using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels.Log;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class LogController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public ActionResult Index(string userName)
        {
            return View(model: userName);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<GetAllLogView> logViews = await _logService.GetAll();
                string jsonResult = JsonConvert.SerializeObject(logViews);
                return Content(jsonResult, "application/json");
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return RedirectToAction("Error", "StartGame", new { message = GameMessageHelper.LogError});
            }
        }
    }
}