namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApp.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Person.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);
            AutomaticMigrationsEnabled = true;
            if (context.User.Where(w => w.IsSuperAdmin && w.IsActive).FirstOrDefault() == null)
            {
                Models.Users user = new Models.Users();
                user.IsActive = user.IsSuperAdmin = true;
                user.IsPasswordResetRequested = user.IsTeam = false;
                user.Name = "Super Admin";
                user.Password = "Password#1";
                user.UserName = "superadmin";
                user.Email = "sa@sports.com";
                user.DateCreated = DateTime.Now;

                Identity.UsersManager userManager = new Identity.UsersManager();
                userManager.CreateAdminUser(user);

            }
        }
    }
}
