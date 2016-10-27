namespace Cellent.Template.Common.Interfaces.Core
{
    /// <summary>
    /// Interface to switch off changenotification
    /// </summary>
    public interface IBindable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is change notification active.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is change notification active; otherwise, <c>false</c>.
        /// </value>
        bool IsChangeNotificationActive { get; set; }
    }
}
