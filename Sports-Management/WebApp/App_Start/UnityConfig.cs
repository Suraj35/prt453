using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.Owin.Security;
using System.Web;
using WebApp.Models;
using WebApp.Services;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using WebApp.Identity;

namespace WebApp.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());


            container.RegisterType<IUserStore<Users, long>, UserStoreService>(new InjectionConstructor(new ApplicationDbContext()));
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IDataContextAsync, StoreDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<IEventsService, EventsService>();
            container.RegisterType<IRepositoryAsync<Events>, Repository<Events>>();

            container.RegisterType<ISportsService, SportsService>();
            container.RegisterType<IRepositoryAsync<Sports>, Repository<Sports>>();

            container.RegisterType<IRepositoryAsync<UserChallenges>, Repository<UserChallenges>>();
            container.RegisterType<IUserChallengesService, UserChallengesService>();

            container.RegisterType<IRepositoryAsync<Venues>, Repository<Venues>>();
            container.RegisterType<IVenueService, VenueService>();

            container.RegisterType<IRepositoryAsync<Notifications>, Repository<Notifications>>();
            container.RegisterType<INotificationsService, NotificationsService>();

            container.RegisterType<IRepositoryAsync<Tickets>, Repository<Tickets>>();
            container.RegisterType<ITicketsService, TicketsService>();

        }
    }
}
