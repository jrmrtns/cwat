using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Context;
using Cellent.Template.Repository.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using Prism.Events;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Repositories
{
    /// <summary>
    /// Repository for resources
    /// </summary>
    [Log]
    public class ResourceRepository : BaseRepository<IResource, ResourceDao>, IResourceRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRepository" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The EventAggregator</param>
        public ResourceRepository(IContextFactory contextFactory, GenericDaoMapper<IResource, ResourceDao> mapper, IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Finds all entries paged for culture information.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<IResource>> FindAllPagedAsync(int page, int pageSize)
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(await context.Resources
                    .OrderBy(d => d.Key)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync());
            }
        }

        /// <summary>
        /// Finds by culture information.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        [Cache(TypeToMonitor = typeof(IResource))]
        public virtual IEnumerable<IResource> FindForCultureInfo(CultureInfo cultureInfo)
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(context.Resources.Where(d => d.Language == cultureInfo.Name).ToList());
            }
        }

        #endregion Methods
    }
}