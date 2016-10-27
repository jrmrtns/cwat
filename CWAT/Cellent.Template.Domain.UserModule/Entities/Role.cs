using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.UserModule.Entities
{
    /// <summary>
    /// Rolle
    /// </summary>
    public partial class Role
    {
        partial void OnConstruct()
        {
            Rights = new Collection<IRight>();
        }

        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        public IEnumerable<IRight> Rights { get; set; }

        /// <summary>
        /// Finds all roles with children asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<IRole>> FindAllWithChildrenAsync()
        {
            return ((IRoleRepository)Repository).FindAllWithChildrenAsync();
        }
    }
}