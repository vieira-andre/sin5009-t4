using System.Web.Mvc;

namespace SIN5009.T4a.Corretora.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "SIN5009 - Corretora WebAPI";

            return View();
        }
    }
}
