using System;
using System.ServiceModel.Configuration;

namespace Cellent.Template.WCF.Behaviors
{
    /// <summary>
    /// ErrorHandlerBehavior
    /// </summary>
    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();

        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        public override Type BehaviorType
        {

            get { return typeof(ErrorServiceBehavior); }
        }
    }
}