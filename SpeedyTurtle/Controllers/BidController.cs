using System;
using System.Web.Mvc;
using Raven.Client.Linq;
using SpeedyTurtle.Models;

namespace SpeedyTurtle.Controllers
{
    public class BidController : RavenController
    {
        public ActionResult SubmitBid(int taskId)
        {
            var task = RavenSession.Load<TurtleTask>(taskId);
            return View(new Bid {TaskId = taskId, TaskDescription = task.Description}); // write the task description in there, just for the list view
        }

        [HttpPost]
        public ActionResult SubmitBid(Bid bid)
        {
            var loggedInAgent = GetLoggedInAgent();
            bid.AgentId = loggedInAgent.Id;
            bid.Status = BidStatus.Pending;
            bid.Submitted = DateTime.UtcNow;
            RavenSession.Store(bid);
            return RedirectToAction("PendingBids");
        }

        public ActionResult PendingBids()
        {
            var pendingBids = RavenSession.Query<Bid>().Where(b => b.Status == BidStatus.Pending);
            return View(pendingBids);
        }
    }
}