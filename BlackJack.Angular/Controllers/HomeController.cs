using System.Web.Mvc;

namespace BlackJack.Angular.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return File(Url.Content("~/Scripts/libs/index.html"), "text/html");
        }
    }
}