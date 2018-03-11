using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebApp.Models;

namespace WebApp.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<Users, long>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

          //public override Task<ClaimsIdentity> CreateUserIdentityAsync(Users user)
          //{
          //    return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
          //}
          
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }


    }
}