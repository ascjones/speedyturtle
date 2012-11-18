using System.Web.Mvc;

namespace SpeedyTurtle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Active = "Home";
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
