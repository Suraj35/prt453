using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Policy;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.HelperClass;
using Repository.Pattern;
using WebApp.ViewModels;

namespace WebApp.Identity
{
    //Main Factory Class and it abstract class
    abstract class FactoryUser
    {
    }
    //The Concrete user class according to pattern
    class GeneralUsers : FactoryUser
    {
    }
    // the creator abstract according to Factory Pattern
    abstract class UserCreator
    {
        public abstract FactoryUser FactoryMethod();
        public abstract List<Users> GetAllUsers();
        public abstract Result<Users> FindById(long Id);
        public abstract Result<long> UpdateUser(Users _user);
        public abstract Result<long> Delete(long userId);


    }

    class UsersManager : UserCreator
    {
        public override FactoryUser FactoryMethod()
        {
            return new GeneralUsers();
        }

        private readonly IOwinContext _iOwinContext = HttpContext.Current.GetOwinContext();
        UserStoreService userStoreService;

        private ApplicationUserManager _userManager;
        public ApplicationUserManager AppUserManager
        {
            get
            {
                return _userManager ?? _iOwinContext.Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UsersManager()
        {
            userStoreService = new UserStoreService();
        }

        public override Result<Users> FindById(long Id)
        {
            Result<Users> result = new Result<Users>();
            Users user = AppUserManager.FindById(Id);
            if (user == null)
            {
                result.success = false;
                result.AddError("User does not exist in system");
            }
            else
            {
                result.data = user;
                result.success = true;
            }
            return result;
        }


        public override Result<long> UpdateUser(Users _user)
        {
            Result<long> result = new Result<long>();
            try
            {
                var update = AppUserManager.Update(_user);
                if (update.Succeeded)
                {
                    UpdateClaims = _user;
                    result.data = _user.Id;
                    result.success = true;
                }
                else
                {
                    result.AddErrors(update.Errors.ToList());
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public override Result<long> Delete(long userId)
        {
            Result<long> result = new Result<long>();
            result = userStoreService.Delete(userId);
            return result;
        }

        public Result<long> Activate(long userId)
        {
            Result<long> result = new Result<long>();
            result = userStoreService.Activate(userId);
            return result;
        }


        public Result<long> CreateUser(Users newUser, ControllerBase controllerBase)
        {
            Result<long> result = new Result<long>();
            newUser.DateCreated = DateTime.Now;
            newUser.IsActive = true;
            var returnVal = AppUserManager.Create(newUser, newUser.Password);
            if (returnVal.Succeeded)
            {
                if (newUser.IsActive)
                {
                    SendWelcomeEmmail(newUser.Id, controllerBase);
                }
                result.success = true;
                result.data = newUser.Id;
            }
            else
            {
                result.AddErrors(returnVal.Errors.ToList<string>());
            }

            return result;

        }


        public Result<long> CreateAdminUser(Users newUser)
        {
            Result<long> result = new Result<long>();
           
            newUser.DateCreated = DateTime.Now;

            newUser.IsActive = true;
            var returnVal = AppUserManager.Create(newUser, newUser.Password);
            if (returnVal.Succeeded)
            {
                result.success = true;
                result.data = newUser.Id;
            }
            else
            {
                result.AddErrors(returnVal.Errors.ToList<string>());
            }

            return result;

        }

        public List<Users> GetAllUsersPaged(int pageId, int pageSize, ref int count)
        {
            Result<List<Users>> result = userStoreService.GetAllUsersPaged(pageId, pageSize, ref count);
            if (result.data == null)
            {
                result.success = false;
                result.AddError("No user found");
                return null;
            }
            return result.data;
        }

        public List<Users> GetAllUsersWithoutCurrent(int eventId, long id,bool team)
        {
            Result<List<Users>> result = userStoreService.GetAllUsersWithoutCurrent(eventId, id, team);
            if (result.data == null)
            {
                result.success = false;
                result.AddError("No user found");
                return null;
            }
            return result.data;
        }

        public override List<Users> GetAllUsers()
        {
            Result<List<Users>> result = userStoreService.GetAllUsers();
            if (result.data == null)
            {
                result.success = false;
                result.AddError("No user found");
                return null;
            }
            return result.data;
        }


        public Result<Users> SignIn(LoginViewModel model)
        {
            Result<Users> result = new Result<Users>();
            Users user = AppUserManager.Find(model.UserName, model.Password);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                    claims.Add(new Claim("UserId", user.Id.ToString()));
                    claims.Add(new Claim("IsTeam", user.IsTeam.ToString()));
                    claims.Add(new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()));
                    try
                    {
                        claims.Add(new Claim("ProfilePic", user.ProfilePic));
                    }
                    catch (Exception)
                    {
                    }
                    var id = new ClaimsIdentity(claims,
                                                DefaultAuthenticationTypes.ApplicationCookie);


                    AuthenticationManager.SignIn(id);

                    user.LastLogin = DateTime.Now;
                    AppUserManager.Update(user);
                    result.data = user;

                }
                else
                {
                    result.success = false;
                    result.AddError("Please Activate your account and try to login.");
                }


            }
            else
            {
                result.success = false;
                result.AddError("Invalid Username or Password.");
            }

            return result;

        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _iOwinContext.Authentication;
            }
        }

        public Result<int> ResetPassword(ResetPasswordViewModel model)
        {
            Result<int> result = new Result<int>();
            try
            {
                Users user = AppUserManager.FindByName(model.UserName);
                if (user == null)
                {
                    result.success = false;
                    result.AddError("User does not exist in system.");
                    return result;
                }

                if (user.IsPasswordResetRequested != true)
                {
                    result.success = false;
                    result.AddError("Your Password Reset Token has been expired, contact system Administrator.");
                    return result;
                }
                var res = AppUserManager.ResetPassword(user.Id, model.Code, model.Password);
                if (res.Succeeded)
                {
                    user.TempPassword = null;
                    AppUserManager.Update(user);
                }
                else
                {
                    result.success = false;
                    result.errors = res.Errors.ToList<string>();
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }

            return result;
        }

        public Result<int> ForgotPassword(ForgotPasswordViewModel model, ControllerBase controllerBase)
        {
            Result<int> result = new Result<int>();
            Users user = AppUserManager.FindByName(model.UserName);
            if (user != null && user.Id > 0 && user.IsActive)
            {
                ForgotPasswordViewModel info = new ForgotPasswordViewModel();
                string code = AppUserManager.GeneratePasswordResetToken(user.Id);

                var callbackUrl = Common.GetUrlHelper().Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Current.Request.Url.Scheme);
                info.Name = user.Name;
                info.Url = callbackUrl;
                info.UserName = user.UserName;
                info.Email= user.Email;

                string defaultPath = "~/Views/Templates/Default/ForgotPassword.cshtml";

                string emailBody = Common.RenderRazorViewToString(defaultPath, info, controllerBase);
                AppUserManager.SendEmail(user.Id, "Reset Password", emailBody);

                user.IsPasswordResetRequested = true;
                AppUserManager.Update(user);


            }
            else
            {

                result.success = false;
                result.AddError("User does not exist in system");
            }






            return result;



        }

        public Result<ResetPasswordViewModel> GetResetPasswordModel(long userId)
        {
            ResetPasswordViewModel model;

            Result<ResetPasswordViewModel> result = new Result<ResetPasswordViewModel>();
            Result<Users> userResult = new Result<Users>();
            userResult = FindById(userId);
            string code = AppUserManager.GeneratePasswordResetToken(userId);
            if (userResult.success)
            {
                model = new ResetPasswordViewModel();
                model.Email = userResult.data.Email;
                model.UserName = userResult.data.UserName;
                model.Code = code;
                result.data = model;

            }
            else
            {
                result.success = false;
                result.AddError(userResult.ErrorMessage);
            }
            return result;

        }

        public Result<int> UpdateResult(ResetPasswordViewModel model)
        {
            Result<int> result = new Result<int>();
            try
            {
                AppUserManager.RemovePassword(Common.CurrentUser.Id);
                AppUserManager.AddPassword(Common.CurrentUser.Id, model.Password);
                Users user = AppUserManager.FindById(Common.CurrentUser.Id);
                user.TempPassword = null;
                AppUserManager.Update(user);
                var identity = new ClaimsIdentity(HttpContext.Current.User.Identity);
                var ctx = _iOwinContext;

                AuthenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant
                (new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }
            return result;

        }

        public Result<int> SendWelcomeEmmail(long userId, ControllerBase controllerBase)
        {
            Result<int> result = new Result<int>();
            try
            {
                Users user = AppUserManager.FindById(userId);
                if (user == null)
                {

                    result.success = false;
                    result.AddError("User does not exist in system");

                }
                else
                {
                    string defaultPath = "~/Views/Templates/Default/WelcomeEmail.cshtml";


                    string emailBody = Common.RenderRazorViewToString(defaultPath, user, controllerBase);
                    AppUserManager.SendEmail(user.Id, "Welcome Email", emailBody);
                    AppUserManager.Update(user);

                }

            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }

            return result;
        }
        public Users UpdateClaims
        {
            set
            {
                var Identity = ClaimsPrincipal.Current.Identities.First();
                var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                try
                {
                    if(Identity.FindFirst("ProfilePic") != null)
                    {
                        Identity.RemoveClaim(Identity.FindFirst("ProfilePic"));
                    }
                    Identity.AddClaim(new Claim("ProfilePic", value.ProfilePic));
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    Identity.RemoveClaim(Identity.FindFirst(ClaimTypes.Name));
                    Identity.RemoveClaim(Identity.FindFirst(ClaimTypes.Email));
                    Identity.RemoveClaim(Identity.FindFirst(ClaimTypes.NameIdentifier));

                    Identity.AddClaim(new Claim(ClaimTypes.Email, value.Email));
                    Identity.AddClaim(new Claim(ClaimTypes.Name, value.Name));
                    Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, value.UserName));
                    AuthenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant
                    (new ClaimsPrincipal(Identity), new AuthenticationProperties { IsPersistent = true });
                }
            }
        }

    }
}