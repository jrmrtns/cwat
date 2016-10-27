using Cellent.Template.Common.Logger;
using Cellent.Template.Service.Properties;
using Cellent.Template.Service.Services;
using Cellent.Template.Service.Startup;
using Cellent.Template.WCF.Behaviors;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace Cellent.Template.Service
{
    /// <summary>
    /// WindowsService für AKV
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class Service : ServiceBase
    {
        private ServiceHost _resourceService;
        private ServiceHost _roleService;
        private ServiceHost _userService;
        private IDisposable _startup;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void StartService(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public void StopService()
        {
            OnStop();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                if (_userService != null)
                {
                    _userService.Close();
                }

                if (_resourceService != null)
                {
                    _resourceService.Close();
                }

                if (_roleService != null)
                {
                    _roleService.Close();
                }

                _userService = new UnityServiceHost(UnityConfig.GetConfiguredContainer(), typeof(UserService));
                _resourceService = new UnityServiceHost(UnityConfig.GetConfiguredContainer(), typeof(ResourceService));
                _roleService = new UnityServiceHost(UnityConfig.GetConfiguredContainer(), typeof(RoleService));

                _userService.Open();
                _resourceService.Open();
                _roleService.Open();

                _startup = WebApp.Start<OwinStartup>(Settings.Default.BaseApiUrl);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw;
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            if (_userService != null)
            {
                _userService.Close();
            }

            _resourceService = null;
            if (_resourceService != null)
            {
                _resourceService.Close();
            }

            _roleService = null;
            if (_roleService != null)
            {
                _roleService.Close();
            }
            _roleService = null;
        }
    }
}