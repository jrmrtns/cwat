using System;
using System.Windows;
using System.Windows.Interactivity;
using Cellent.Template.Client.Views;
using Prism.Interactivity.InteractionRequest;

namespace Cellent.Template.Client.WindowActions
{
    /// <summary>
    /// ConfirmationWindowAction
    /// </summary>
    public class ConfirmationWindowAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            InteractionRequestedEventArgs args = parameter as InteractionRequestedEventArgs;
            if (args != null)
            {
                ViewModels.MainWindowViewModel.ConfirmationWrapper confirmation = args.Context as ViewModels.MainWindowViewModel.ConfirmationWrapper;
                if (confirmation != null)
                {
                    ConfirmationDialog window = new ConfirmationDialog(confirmation);
                    EventHandler closeHandler = null;
                    closeHandler = (sender, e) =>
                    {
                        window.Closed -= closeHandler;
                        args.Callback();
                    };
                    window.Closed += closeHandler;
                    window.ShowDialog();
                }
            }
        }
    }
}
