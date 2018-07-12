using Cellent.Template.Client.Core.Core;
using Cellent.Template.ClientModule.User.ViewModels;
using Cellent.Template.ClientModule.User.Views;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Interceptors;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Modularity;
using System.ComponentModel;
using System.Diagnostics;
using CreateUserView = Cellent.Template.ClientModule.User.Views.CreateUserView;

namespace Cellent.Template.ClientModule.User
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
        public Module(IUnityContainer container) : base(container)
        {
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Container.RegisterType<object, CreateUserView>(Constants.ViewNames.CreateUserView);
            Container.RegisterType<object, UserList>(Constants.ViewNames.ListUser);

            Container.RegisterType<CreateUserViewModel>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new AdditionalInterface<INotifyPropertyChanged>(),
                new InterceptionBehavior<NotifyPropertyChangedBehavior>());

            Container.RegisterType<UserListViewModel>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new AdditionalInterface<INotifyPropertyChanged>(),
                new InterceptionBehavior<NotifyPropertyChangedBehavior>());
        }
    }
}