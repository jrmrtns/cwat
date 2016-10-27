using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Cellent.Template.Client.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            Message.Text = string.Format("Version {0} - Initialisierung...", Assembly.GetExecutingAssembly().GetName().Version);

            Disclaimer.Text = string.Format("© {0} Release, all rights reserved", DateTime.Now.Year);
        }

        private async Task Loop()
        {
            await Task.Delay(100);
            Message.Text = string.Format("Version {0} - Anmeldung...", Assembly.GetExecutingAssembly().GetName().Version);

            await Task.Delay(1000);
            Message.Text = string.Format("Version {0} - Abrufen der Userinformationen...", Assembly.GetExecutingAssembly().GetName().Version);
        }


        private async void Message_Loaded(object sender, RoutedEventArgs e)
        {          
            await Loop();
        }
    }
}
