using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedyTurtle.Models;

namespace SpeedyTurtle.Controllers
{
    public class TaskController : RavenController
    {
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(TurtleTask turtleTask)
        {
            RavenSession.Store(turtleTask);
            return RedirectToAction("Details", new {id = turtleTask.Id});
        }

        public ActionResult Details(int id)
        {
            var task = RavenSession.Load<TurtleTask>(id);
            return View(task);
        }
    }
}