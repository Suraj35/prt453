using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.IO;
using System.Web.Mvc;
using WebApp.ViewModels;
using System.Web.Routing;
using Repository.Pattern;
using System.Web.Configuration;

namespace WebApp.HelperClass
{
    public class Common
    {
        public static UserInfo CurrentUser
        {
            get
            {
                UserInfo user = new UserInfo();
                try
                {
                    
                    var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

                    user.Id = long.Parse(claimsIdentity.FindFirst("UserId").Value);
                    user.Name = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
                    user.Email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;
                    user.UserName = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    user.IsTeam = Convert.ToBoolean(claimsIdentity.FindFirst("IsTeam").Value);
                    user.IsSuperAdmin = Convert.ToBoolean(claimsIdentity.FindFirst("IsSuperAdmin").Value);
                    user.ProfilePic = claimsIdentity.FindFirst("ProfilePic").Value;
                    return user;
                }
                catch (Exception e)
                {
                    return user;
                }
            }
        }

        public static UrlHelper GetUrlHelper()
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            return new UrlHelper(new RequestContext(httpContext, CurrentRoute(httpContext)));
        }
        public static RouteData CurrentRoute(HttpContextWrapper httpContext)
        {
            return RouteTable.Routes.GetRouteData(httpContext);
        }

        public static string RenderRazorViewToString(string viewName, object model, ControllerBase controllerBase)
        {

            controllerBase.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerBase.ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(controllerBase.ControllerContext, viewResult.View,
                                            controllerBase.ViewData, controllerBase.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerBase.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


        public static Result<string> SaveProfileImage(string id, HttpPostedFileBase file)
        {
            Result<string> result = new Result<string>();
            //Save image local
            try
            {
                string folder = WebConfigurationManager.AppSettings["MediaFolder"];
                string domain = WebConfigurationManager.AppSettings["DomainPath"];
                if (!(Directory.Exists(HttpContext.Current.Server.MapPath(folder + id+ "/"))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folder + id + "/"));
                }

                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder + id + "/" + file.FileName));

                if (file != null && file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".gif"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
                    {
                        result.success = false;
                        result.AddError("Please Select an image file.");
                        return result;
                    }
                    if (path != null && File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    
                    file.SaveAs(path);

                    path = folder + id + "/"+ file.FileName.Replace("\\", "/");
                    result.data = path;
                }
                result.success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
                return result;
            }
        }
    }
}