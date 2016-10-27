using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.Constants;
using System.Collections.Generic;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Global client settings context
    /// </summary>
    public class ClientContext
    {
        /// <summary>
        /// Gets or sets currently logged user
        /// </summary>
        public static IUserModel CurrentUser { get; set; }

        /// <summary>
        /// The client rights
        /// </summary>
        public static IEnumerable<ClientRights> ClientRights { get; set; }
    }
}