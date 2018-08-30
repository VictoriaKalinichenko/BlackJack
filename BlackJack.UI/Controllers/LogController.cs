using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackJack.ViewModels.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
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

        public async Task<ActionResult> Index()
        {
            try
            {
                IEnumerable<LogViewModel> logViewModels = await _logService.GetAll();
                return View(logViewModels);
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