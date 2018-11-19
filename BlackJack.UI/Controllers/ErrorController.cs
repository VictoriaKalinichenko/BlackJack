using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Display(string message)
        {
            return View("~/Views/Error/Display.cshtml", null, message);
        }
    }
}