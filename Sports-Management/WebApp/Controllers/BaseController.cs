
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [CustomAuthorize]
    public class BaseController : Controller
    {
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
    }
}