using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Models;

namespace WebApp
{
    //Update-Database -configuration WebApp.Migrations.Configuration -Verbose -force

    public class StoreDbContext : Repository.Pattern.Ef6.DataContext
    {
        //Update-Database -configuration WebApp.Migrations.Configuration -Verbose -force
        public StoreDbContext()
            : base("DefaultConnection")
        {
        }

        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Venues> Venues { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<UserChallenges> UserChallenges { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
    }
}