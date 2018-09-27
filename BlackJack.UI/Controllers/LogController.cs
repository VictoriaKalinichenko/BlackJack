using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.Log;
using NLog;
using Newtonsoft.Json;

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
                IEnumerable<GetAllViewModel> logViews = await _logService.GetAll();
                var jsonResult = new JsonResult();
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                jsonResult.MaxJsonLength = 5000000;
                jsonResult.Data = logViews;
                return jsonResult;
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