using System.Linq;
using System.Web.Mvc;
using SpeedyTurtle.Models;
using SpeedyTurtle.Models.ViewModels;

namespace SpeedyTurtle.Controllers
{
    public class BidController : RavenController
    {
        public ActionResult PendingBids()
        {
            var agent = GetLoggedInUser();
            // todo: [AJ] ugh, this is ugly and inefficient - consider remodelling, or building an index?
            // yuck yuck yuck
            var tasks = RavenSession.Query<UserTask>().Where(t => t.Bids != null && t.Bids.Any(b => b.Agent.Id == agent.Id)).ToArray();
            var bids = tasks.Select(t =>
            {
                var bid = t.Bids.Single(b => b.Agent.Id == agent.Id);
                return new AgentBidViewModel {TaskDescription = t.Description, Submitted = bid.Submitted};
            });

            return View(bids);
        }
    }
}