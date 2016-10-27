using System;
using Prism.Interactivity.InteractionRequest;

namespace Cellent.Template.Client.Core.Interfaces
{
    /// <summary>
    /// Interface für MessageBoxen
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">Title of Messagebox</param>
        /// <param name="callback">The callback.</param>
        void ShowDialog(String message, String title, Action<Confirmation> callback);

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">Title of Messagebox</param>
        void ShowDialog(String message, String title);
    }
}
