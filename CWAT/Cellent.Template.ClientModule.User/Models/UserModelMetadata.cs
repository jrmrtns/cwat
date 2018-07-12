using Cellent.Template.Common.Interceptors.Helper;
using System;

namespace Cellent.Template.ClientModule.User.Models
{
    /// <summary>
    /// UserModelMetadata um zusätzliche Attribute angeben zu können
    /// </summary>
    public class UserModelMetadata
    {
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        [OnPropertyChanged("Name")]
        public virtual String Firstname { get; set; }
    }
}