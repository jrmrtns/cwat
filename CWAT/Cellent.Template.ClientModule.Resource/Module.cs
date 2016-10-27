using Cellent.Template.Client.Core.Core;
using Cellent.Template.ClientModule.Resource.ViewModels;
using Cellent.Template.ClientModule.Resource.Views;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Interceptors;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Modularity;
using System.ComponentModel;
using System.Diagnostics;
using CreateResourceView = Cellent.Template.ClientModule.Resource.Views.CreateResourceView;

namespace Cellent.Template.ClientModule.Resource
{
    /// <summary>
    /// Beschreibt das UserModule
    /// </summary>
    [Module(ModuleName = Constants.Modules.UserModule)]
    public class Module : BaseModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public Module(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Container.RegisterType<object, ResourceList>(Constants.ViewNames.ResourceList);

            Container.RegisterType<ResourceListViewModel>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new AdditionalInterface<INotifyPropertyChanged>(),
                new InterceptionBehavior<NotifyPropertyChangedBehavior>());

            Container.RegisterType<object, CreateResourceView>(Constants.ViewNames.CreateResource);

            Container.RegisterType<CreateResourceViewModel>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new AdditionalInterface<INotifyPropertyChanged>(),
                new InterceptionBehavior<NotifyPropertyChangedBehavior>());
        }
    }
}