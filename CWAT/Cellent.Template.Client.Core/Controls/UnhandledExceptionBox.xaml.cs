using System;
using System.Windows;

namespace Cellent.Template.Client.Core.Controls
{
    /// <summary>
    /// Interaktionslogik für UnhandledExceptionDialog.xaml
    /// </summary>
    public partial class UnhandledExceptionBox
    {
        private Exception _exception;
        private string _message;

        /// <summary>
        /// Constructor
        /// </summary>
        public UnhandledExceptionBox(string message, Exception exception)
        {
            InitializeComponent();

            _message = message;
            _exception = exception;

            Loaded += UnhandledExceptionBox_Loaded;
        }

        /// <summary>
        /// Handels loaded event
        /// </summary>
        private void UnhandledExceptionBox_Loaded(object sender, RoutedEventArgs e)
        {
            txtBlockMessage.Text = _message;
            txtBlockExceptionMessage.Text = _exception.Message;
            txtBlockExceptionDetails.Text = _exception.ToString();
        }

        private void ButtonDetails_Click(object sender, RoutedEventArgs e)
        {
            if (txtBlockExceptionDetails.Visibility == Visibility.Collapsed)
                txtBlockExceptionDetails.Visibility = Visibility.Visible;
            else
                txtBlockExceptionDetails.Visibility = Visibility.Collapsed;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSendFeedback_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}