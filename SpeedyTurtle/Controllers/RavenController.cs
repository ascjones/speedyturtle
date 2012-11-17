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

        protected Agent GetLoggedInAgent()
        {
            return RavenSession.Query<Agent>().SingleOrDefault(a => a.Username == User.Identity.Name);
        }
    }
}