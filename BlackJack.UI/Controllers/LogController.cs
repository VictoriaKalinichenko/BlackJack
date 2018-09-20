using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

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
        public async Task<ActionResult> GetLogs()
        {
            try
            {
                IEnumerable<LogGetLogsView> logViews = await _logService.GetAllLogs();
                return Json(logViews, JsonRequestBehavior.AllowGet);
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