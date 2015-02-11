#region Using directives

using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Skarpline.CommonLayer.Mapper;
using Skarpline.CommonLayer.Unity;
using Skarpline.Models;

#endregion

namespace Skarpline.API
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

            RegisterApplicationDBContext(container);
            SkarplineUnityContainer.RegisterTypes(container);

            InitializeObjectMapper(container);

            return container;
        });

        public static IUnityContainer Container { get; set; }

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        private static void RegisterApplicationDBContext(IUnityContainer container)
        {

            //TODO...see why UserManager is getting disposed with any built in Lifetime Managers
            //container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            //container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
        }

        private static void InitializeObjectMapper(IUnityContainer container)
        {
            container.Resolve<IObjectMapper>().CreateMap();
        }

        #endregion
    }
}