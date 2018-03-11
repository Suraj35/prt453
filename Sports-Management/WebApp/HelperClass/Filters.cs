using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApp.HelperClass;

namespace WebApp.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute() : base()
        {
        }
        //use as attribute for actions, controllers 
        //[AuthorizeRoles(Role.Administrator, Role.Assistant)]
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = base.AuthorizeCore(httpContext);

            if (!authorized)
            {
                HttpContext.Current.Response.Redirect("/Account/Login");
            }
            return authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("UpdatePassword", "Account"));
            }
            else
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Account"));
            }
        }
    }

    public class SuperAdminAuthorizeAttribute : AuthorizeAttribute
    {
        public SuperAdminAuthorizeAttribute() : base()
        {
        }
        //use as attribute for actions, controllers 
        //[AuthorizeRoles(Role.Administrator, Role.Assistant)]
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = base.AuthorizeCore(httpContext);

            if (!authorized || !Common.CurrentUser.IsSuperAdmin)
            {
                HttpContext.Current.Response.Redirect("/Account/Login");
            }
            return authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("UpdatePassword", "Account"));
            }
            else
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Account"));
            }
        }
    }
}