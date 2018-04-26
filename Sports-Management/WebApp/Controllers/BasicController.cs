
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    abstract class BasicController : Controller
    {
        public BasicController(List<string> fault1, string memo )
        {
            fault1 = null;
            memo = "Some Error has happen";

        }

        public void AddErrors(List<string> errors, string message)
        {
            if (errors == null || errors.Count == 0)
            {
                ModelState.AddModelError("", message);
            }
            else
            {
                foreach (string s in errors)
                {
                    ModelState.AddModelError("", s);
                }
            }
        }

        public abstract BasicController Clone();

    }
}
