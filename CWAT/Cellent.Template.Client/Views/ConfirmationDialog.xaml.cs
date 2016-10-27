using System;
using System.Windows;
using Cellent.Template.Client.ViewModels;
using Cellent.Template.Common.Constants;

namespace Cellent.Template.Client.Views
{
    
    /// <summary>
    /// Interaction logic for ConfirmationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmationDialog"/> class.
        /// </summary>
        public ConfirmationDialog()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmationDialog"/> class.
        /// </summary>
        /// <param name="confirmation">The confirmation.</param>
        public ConfirmationDialog(MainWindowViewModel.ConfirmationWrapper confirmation )
            : this()
        {
            DataContext = confirmation;

            switch (confirmation.ConfirmationButtons) {
                case Constants.ConfirmationButtons.Ok:
                    cancelButton.Visibility = Visibility.Hidden;
                    break;
                case Constants.ConfirmationButtons.OkCancel:
                    cancelButton.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new NotImplementedException();
            };

        }
    }
}
