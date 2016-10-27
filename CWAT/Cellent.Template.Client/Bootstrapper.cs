using Cellent.Template.Client.Core.Controls;
using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Core.Resources;
using Cellent.Template.Client.Core.Interfaces;
using Cellent.Template.Client.Core.Interfaces.Factories;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Client.Splash;
using Cellent.Template.Client.ViewModels;
using Cellent.Template.Client.Views;
using Cellent.Template.ClientModule.User;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Common.ServiceClient;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Cellent.Template.Client
{
    /// <summary>
    /// Initailisiert die Shell und registriert abhängige Klassen
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Lädt und konfiguriert den Container
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.AddNewExtension<Interception>();

            RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
            Container.RegisterType<INotificationService, MainWindowViewModel>(new ContainerControlledLifetimeManager());

            Container.RegisterType<MainWindowViewModel>(
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior(new LoggingBehavior(TraceEventType.Verbose)),
                new AdditionalInterface<INotifyPropertyChanged>(),
                new InterceptionBehavior<NotifyPropertyChangedBehavior>());

            ModuleManager moduleManager = Container.TryResolve<ModuleManager>();
            moduleManager.LoadModule(Constants.Modules.UserModule);
            moduleManager.LoadModule(Constants.Modules.ResourceModule);
        }

        /// <summary>
        /// Lädt und konfiguriert den ModuleCatalog
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            Type module = typeof(Module);
            ModuleCatalog.AddModule(
              new ModuleInfo
              {
                  ModuleName = Constants.Modules.UserModule,
                  ModuleType = module.AssemblyQualifiedName,
              });

            module = typeof(ClientModule.Resource.Module);
            ModuleCatalog.AddModule(
              new ModuleInfo
              {
                  ModuleName = Constants.Modules.ResourceModule,
                  ModuleType = module.AssemblyQualifiedName,
              });
        }

        /// <summary>
        /// Erzeugt eine neue Shell
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            try
            {
                Task<LoadClientResult> contextTask = Task.Run(async () => { return await LoadClientContext(); });
                contextTask.Wait();
                var result = contextTask.Result;
            }
            catch (AggregateException exception)
            {
                SplashScreenHelper.CloseSplash();
                MessageBox.Show(exception.InnerException.Message);
                return null;
            }

            ViewModelLocationProvider.SetDefaultViewModelFactory(t =>
            {
                return Container.Resolve(t);
            });
            MainWindow view = Container.TryResolve<MainWindow>();
            return view;
        }

        /// <summary>
        /// Initialisiert die Shell
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        protected override void InitializeShell()
        {
            base.InitializeShell();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            Application.Current.MainWindow = (Window)Shell;
            ShowMainWindow();
            SplashScreenHelper.CloseSplash();
        }

        [HandleProcessCorruptedStateExceptions]
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Common.Logger.Logger.Write(e.ExceptionObject as Exception);
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Common.Logger.Logger.Write(e.Exception);

            UnhandledExceptionBox box = new UnhandledExceptionBox(
                Translation.Translate("UnhandledExceptionMessage").Replace("\\n", Environment.NewLine),
                e.Exception);

            box.ShowDialog();

            e.Handled = true;
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Lädt ClientContext Data
        /// </summary>
        private async Task<LoadClientResult> LoadClientContext()
        {
            LoadClientResult loadClientRes = new LoadClientResult();
            try
            {
                IModelFactory<IUserModel, UserDto> factory = Container.Resolve<IModelFactory<IUserModel, UserDto>>();

                Tuple<UserDto, IEnumerable<ClientRights>> userInformation = await ServiceClient<IUserService>.ExecuteAsync(d => d.GetCurrentUserAsync());

                ClientContext.CurrentUser = factory.Convert(userInformation.Item1);
                ClientContext.ClientRights = userInformation.Item2;

                loadClientRes.IsSuccessfull = true;
                Constants.UserId = ClientContext.CurrentUser.Id;
            }
            catch (FaultException<RemoteFault> exception)
            {
                FaultException<RemoteFault> faultException = exception;
                loadClientRes.FaultId = faultException.Detail.FaultId;
                Common.Logger.Logger.Write(exception);
                loadClientRes.IsSuccessfull = false;
            }
            catch (EndpointNotFoundException exception)
            {
                Common.Logger.Logger.Write(exception);
                loadClientRes.IsSuccessfull = false;
                throw;
            }
            catch (Exception ex)
            {
                Common.Logger.Logger.Write(ex);
                loadClientRes.IsSuccessfull = false;
                throw;
            }

            return loadClientRes;
        }

        private void ShowMainWindow()
        {
            Application.Current.MainWindow.Show();
        }
    }
}