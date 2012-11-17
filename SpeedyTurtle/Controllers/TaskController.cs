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
        public ActionResult Submit(UserTask userTask)
        {
            RavenSession.Store(userTask);
            return RedirectToAction("Details", new {id = userTask.Id});
        }

        public ActionResult Details(int id)
        {
            var task = RavenSession.Load<UserTask>(id);
            return View(task);
        }

        public ActionResult List()
        {
            // all tasks for agents to peruse
            var allTasks = RavenSession.Query<UserTask>();
            return View(allTasks);
        }
    }
}