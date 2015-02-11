#region Using directives

using Microsoft.Practices.Unity;
using Skarpline.BusinessLayer.Service.Message;
using Skarpline.BusinessLayer.Service.User;
using Skarpline.BusinessLayer.ServiceImpl.Message;
using Skarpline.BusinessLayer.ServiceImpl.Users;
using Skarpline.CommonLayer.Mapper;
using Skarpline.PersistenceLayer.EF.EDMX;
using Skarpline.PersistenceLayer.EF.Impl;
using Skarpline.PersistenceLayer.Repository.DataContext;
using Skarpline.PersistenceLayer.Repository.Repositories;
using Skarpline.PersistenceLayer.Repository.UnitOfWork;

#endregion

namespace Skarpline.CommonLayer.Unity
{
    public class SkarplineUnityContainer
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IObjectMapper, ObjectMapper>();

            //call method to register business services
            RegisterBusinessServices(container);

            ////call method to register Persistence Layer clases
            RegisterPersistenceRepository(container);
        }

        private static void RegisterPersistenceRepository(IUnityContainer container)
        {
            #region Registration of Persistence Layer Repository Classes

            container.RegisterType<IDataContextAsync, Entities>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryImpl<>));
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>();

            #endregion
        }

        private static void RegisterBusinessServices(IUnityContainer container)
        {
            container.RegisterType<IUserService, UserServiceImpl>(new InjectionConstructor(typeof(IUnitOfWorkAsync)));
            container.RegisterType<IMessagesService, MessagesServiceImpl>(new InjectionConstructor(typeof(IUnitOfWorkAsync)));
        }
    }
}