using BlackJack.ViewModels.Error;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Display(string message)
        {
            var view = new DisplayErrorView();
            view.Message = message;
            return View(view);
        }
    }
}