using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Client.Core.Interfaces.Models
{
    /// <summary>
    /// Interface für Rollen
    /// </summary>
    public partial interface IRoleModel
    {
        /// <summary>
        /// Gets or sets the right groups.
        /// </summary>
        /// <value>
        /// The right groups.
        /// </value>
        IEnumerable<IRightModel> Rights { get; set; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        Task<IRoleModel> SaveAsync();
    }
}