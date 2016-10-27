using Cellent.Template.Client.Views;

namespace Cellent.Template.Client.Splash
{
    /// <summary>
    /// Helper to show or close given splash window
    /// </summary>
    public static class SplashScreenHelper
    {
        /// <summary>
        /// Get or set the splash screen window
        /// </summary>
        public static SplashScreen Splash { private get; set; }

        /// <summary>
        /// Show splash screen
        /// </summary>
        public static void ShowSplash()
        {
            if (Splash != null)
            {
                Splash.Show();
            }
        }
        /// <summary>
        /// Close splash screen
        /// </summary>
        public static void CloseSplash()
        {
            if (Splash != null)
            {
                Splash.Close();
            }
        }
    }
}
