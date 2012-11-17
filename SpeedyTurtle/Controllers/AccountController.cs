using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SpeedyTurtle.Models;

namespace SpeedyTurtle.Controllers
{
    public class AccountController : RavenController
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (AuthenticateAgent(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool AuthenticateAgent(string username, string password)
        {
            var agent = GetAgentByUsername(username);
            if (agent != null)
                return agent.Password == password;
            return false;
        }

        private Agent GetAgentByUsername(string username)
        {
            return RavenSession.Query<Agent>().SingleOrDefault(a => a.Username == username);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var existingAgent = GetAgentByUsername(model.UserName);

                if (existingAgent == null)
                {
                    RavenSession.Store(new Agent
                    {
                        Username = model.UserName, 
                        Email = model.Email,
                        Password = model.Password
                    }); // todo: add other fields
                    FormsAuthentication.SetAuthCookie(model.UserName, true /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                // user with the same name has already been created
                ModelState.AddModelError("", "User name already exists. Please enter a different user name");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
