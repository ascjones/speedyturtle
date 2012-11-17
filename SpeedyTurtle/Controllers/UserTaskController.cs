using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedyTurtle.Models;
using SpeedyTurtle.Models.ViewModels;

namespace SpeedyTurtle.Controllers
{
    public class UserTaskController : RavenController
    {
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(UserTaskViewModel viewModelTask)
        {
            var task = new UserTask
            {
                Title = viewModelTask.Title, 
                Description = viewModelTask.Description, 
                MaximumOffer = viewModelTask.MaximumOffer, 
                Submitted = DateTime.UtcNow
            };
            RavenSession.Store(task);
            return RedirectToAction("Details", new { id = task.Id });
        }

        public ActionResult Details(int id)
        {
            var task = RavenSession.Load<UserTask>(id);
            var viewModel = CreateTaskViewModel(task);

            viewModel.Bids = RavenSession.Query<Bid>()
                .Where(b => b.TaskId == task.Id && b.Status == BidStatus.Pending)
                .Select(b => new UserTaskBidViewModel { Id = b.Id, AgentId = b.AgentId, Amount = b.Amount }) // todo: get Agent name - join somehow?
                .ToArray();

            return View(viewModel);
        }

        private static UserTaskViewModel CreateTaskViewModel(UserTask task)
        {
            var viewModel = new UserTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                MaximumOffer = task.MaximumOffer
            };
            return viewModel;
        }

        public ActionResult List()
        {
            // all tasks for agents to peruse
            var allTasks = RavenSession.Query<UserTask>().ToArray().Select(CreateTaskViewModel).ToArray();
            return View(allTasks);
        }
    }
}