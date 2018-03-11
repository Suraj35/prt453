using System.Web.Mvc;
using WebApp.HelperClass;
using WebApp.Identity;
using Repository.Pattern;
using WebApp.Models;
using AutoMapper;
using WebApp.ViewModels;
using System.Web;
using System.Collections.Generic;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    
    public class UserController : BaseController
    {
        UsersManager _usermanager;
        public UserController()
        {
            _usermanager = new UsersManager();
        }
        [SuperAdminAuthorizeAttribute]
        public ActionResult Index()
        {
            List<Users> model = new List<Users>();
            model = _usermanager.GetAllUsers();
            return View(model);
        }


        // GET: User
        public ActionResult EditProfile()
        {

            var userId = Common.CurrentUser.Id;

            Result<Users> user = _usermanager.FindById(userId);

            RegisterViewModel registerViewModel = new RegisterViewModel();
            Mapper.Map(user.data, registerViewModel);

            if (user.success)
            {
                return View(registerViewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult EditProfile(RegisterViewModel registerModel, HttpPostedFileBase FileUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            Result<string> result = new Result<string>();
            Users user = new Users();
            Result<long> res = new Result<long>();
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                result = Common.SaveProfileImage(registerModel.Id.ToString(), FileUpload);
                if (result.success)
                {
                    registerModel.ProfilePic = result.data;
                }
            }
            Mapper.Map(registerModel, user);
            res = _usermanager.UpdateUser(user);
            if (res.success)
            {
                TempData["SuccessMessage"] = "User updated Successfully.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AddErrors(res.errors, res.ErrorMessage);
            }
            return View(registerModel);
        }
        [SuperAdminAuthorizeAttribute]
        public ActionResult Delete(int id)
        {
            var result = _usermanager.Delete(id);
            return RedirectToAction("Index");
        }
        [SuperAdminAuthorizeAttribute]
        public ActionResult Activate(int id)
        {
            var result = _usermanager.Activate(id);
            return RedirectToAction("Index");
        }
    }
}