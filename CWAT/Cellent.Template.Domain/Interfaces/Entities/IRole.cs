using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Interfaces.Entities
{
    public partial interface IRole
    {
        /// <summary>
        /// Gets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        IEnumerable<IRight> Rights { get; set; }

        /// <summary>
        /// Finds all roles with children asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IRole>> FindAllWithChildrenAsync();
    }
}