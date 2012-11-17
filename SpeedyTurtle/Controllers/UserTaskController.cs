using System;
using System.Linq;
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

            return View(viewModel);
        }

        public ActionResult AcceptBid(int taskId, int bidId)
        {
            var task = RavenSession.Load<UserTask>(taskId);

            task.AcceptWinningBid(bidId);

            return RedirectToAction("Details", "UserTask"); // todo: [AJ] change the display of the user task if it is in progreee
        }

        private static UserTaskViewModel CreateTaskViewModel(UserTask task)
        {
            var userTaskBidViewModels = task.Bids == null ? new UserTaskBidViewModel[]{} : task.Bids.Select(b => new UserTaskBidViewModel
            {
                Id = b.Id, AgentId = b.Agent.Id, AgentName = b.Agent.Name, Amount = b.Amount,
            }).ToArray();

            var viewModel = new UserTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                MaximumOffer = task.MaximumOffer,
                Bids = userTaskBidViewModels
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