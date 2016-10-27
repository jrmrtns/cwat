using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Service.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Events;
using System;

namespace Cellent.Template.Service.Startup
{
    /// <summary>
    /// Konfiguration für den IoC Container Unity
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
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
            return Container.Value;
        }

        #endregion Unity Container

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());

            container.AddNewExtension<Interception>();

            container.RegisterType<UserService>();
            container.Configure<Interception>()
                .SetInterceptorFor<UserService>(new VirtualMethodInterceptor());

            container.RegisterType<ResourceService>();
            container.Configure<Interception>()
                .SetInterceptorFor<ResourceService>(new VirtualMethodInterceptor());

            container.RegisterType<RoleService>();
            container.Configure<Interception>()
                .SetInterceptorFor<RoleService>(new VirtualMethodInterceptor());

            container.RegisterType<Domain.UserModule.Module>();
            container.Resolve<Domain.UserModule.Module>().Initialize();

            container.RegisterType<Domain.ResourceModule.Module>();
            container.Resolve<Domain.ResourceModule.Module>().Initialize();

            container.RegisterType<Repository.Module>();
            container.Resolve<Repository.Module>().Initialize();

            container.RegisterType<Domain.MasterDataModule.Module>();
            container.Resolve<Domain.MasterDataModule.Module>().Initialize();

            container.RegisterType(typeof(EntityChangeMonitor<>));
        }
    }
}