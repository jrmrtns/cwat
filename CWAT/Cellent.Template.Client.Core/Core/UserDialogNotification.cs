using Prism.Interactivity.InteractionRequest;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Erweiterung des Confirmation Objektes
    /// </summary>
    public class UserDialogNotification : Confirmation
    {
        /// <summary>
        /// Gets or sets a value indicating whether [add internal user].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add internal user]; otherwise, <c>false</c>.
        /// </value>
        public bool AddInternalUser { get; set; }
    }
}
