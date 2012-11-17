using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SpeedyTurtle.Models;
using SpeedyTurtle.Models.ViewModels;

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

        public ActionResult SubmitBid(int taskId)
        {
            var task = RavenSession.Load<UserTask>(taskId);
            return View(new AgentBidViewModel{TaskId = task.Id}); // write the task description in there, just for the list view
        }

        [HttpPost]
        public ActionResult SubmitBid(AgentBidViewModel viewModelBid)
        {
            var loggedInAgent = GetLoggedInAgent();
            var task = RavenSession.Load<UserTask>(viewModelBid.TaskId);

            task.SubmitBid(new Bid
            {
                Agent = new AgentDetail
                {
                    Id = loggedInAgent.Id,
                    Name = string.Format("{0} {1}", loggedInAgent.FirstName, loggedInAgent.LastName)
                },
                Amount = viewModelBid.Amount,
                Comments = viewModelBid.Comments,
                Status = BidStatus.Pending,
                Submitted = DateTime.UtcNow
            });

            return RedirectToAction("PendingBids", "Bid", new {id = task.Id});
        }
    }
}