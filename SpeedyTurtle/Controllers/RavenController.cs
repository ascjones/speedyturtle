using System.Web.Mvc;
using Raven.Client;
using SpeedyTurtle.Models;
using System.Linq;

namespace SpeedyTurtle.Controllers
{
    public class RavenController : Controller
    {
        public IDocumentSession RavenSession { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.Store.OpenSession();

            var loggedInAgent = GetLoggedInUser();
            if (loggedInAgent != null)
                ViewBag.UserType = loggedInAgent.Type;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }

        protected User GetLoggedInUser()
        {
            return RavenSession.Query<User>().SingleOrDefault(a => a.Username == User.Identity.Name);
        }
    }
}