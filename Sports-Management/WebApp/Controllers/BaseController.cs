
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [CustomAuthorize]
    abstract class BaseController : Controller
    {
        public abstract void AddErrors(List<string> errors, string message);

    }
}