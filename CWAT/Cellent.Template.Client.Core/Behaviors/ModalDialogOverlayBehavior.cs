using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace Cellent.Template.Client.Core.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public class ModalDialogOverlayBehavior : Behavior<Rectangle>
    {
        /// <summary>
        /// Gets or sets the overlay.
        /// </summary>
        /// <value>
        /// The overlay.
        /// </value>
        public Rectangle Overlay { get; set; }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            Overlay = AssociatedObject as Rectangle;
            ComponentDispatcher.EnterThreadModal += ComponentDispatcher_EnterThreadModal;
            ComponentDispatcher.LeaveThreadModal += ComponentDispatcher_LeaveThreadModal;
        }

        void ComponentDispatcher_LeaveThreadModal(object sender, EventArgs e)
        {
            Overlay.Visibility = Visibility.Collapsed;
        }

        void ComponentDispatcher_EnterThreadModal(object sender, EventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;
        }
    }
}
