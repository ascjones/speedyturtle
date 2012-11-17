using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyTurtle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Speedy Turtles!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
