using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Cellent.Template.Domain.Interfaces.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Interfaces.Repositories
{
    /// <summary>
    /// Repository für Rollen
    /// </summary>
    public interface IRoleRepository : IBaseRepository<IRole>
    {
        /// <summary>
        /// Finds all with children asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IRole>> FindAllWithChildrenAsync();
    }
}