using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Cellent.Template.Domain.Interfaces.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Interfaces.Repositories
{
    /// <summary>
    /// Resource Repository
    /// </summary>
    [Log]
    public interface IResourceRepository : IBaseRepository<IResource>
    {
        /// <summary>
        /// Finds for culture information.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        IEnumerable<IResource> FindForCultureInfo(CultureInfo cultureInfo);

        /// <summary>
        /// Finds for culture information.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<IEnumerable<IResource>> FindAllPagedAsync(int page, int pageSize);
    }
}