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
            var task = RavenSession.Load<UserTask>(taskId);
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

        public ActionResult AcceptBid(int bidId)
        {
            var bid = RavenSession.Load<Bid>(bidId);
            var task = RavenSession.Load<UserTask>(bid.TaskId);

            task.AcceptWinningBid(bid);

            return RedirectToAction("Details", "UserTask"); // todo: [AJ] change the display of the user task if it is in progreee
        }

        public ActionResult PendingBids()
        {
            var loggedInAgent = GetLoggedInAgent();
            var pendingBids = RavenSession.Query<Bid>().Where(b => b.AgentId == loggedInAgent.Id && b.Status == BidStatus.Pending);
            return View(pendingBids);
        }
    }
}