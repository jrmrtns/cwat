using Cellent.Template.Client.Splash;
using Cellent.Template.Common.Constants;
using System;
using System.Windows;
using SplashScreen = Cellent.Template.Client.Views.SplashScreen;

namespace Cellent.Template.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        ///
        /// </summary>
        [STAThread()]
        private static void Main(string[] args)
        {
            SplashScreenHelper.Splash = new SplashScreen();
            SplashScreenHelper.ShowSplash();

            App app = new App();
            app.InitializeComponent();

            if (args != null && args.Length > 0)
            {
                Constants.OnBehalfUser = args[0];
            }

            app.Run();
        }

        /// <summary>
        /// Programmstart
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}