using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Context;
using Cellent.Template.Repository.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using Prism.Events;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Repositories
{
    /// <summary>
    /// RoleRepository
    /// </summary>
    public class RoleRepository : BaseRepository<IRole, RoleDao>, IRoleRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="contextFactory"></param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator"></param>
        public RoleRepository(IContextFactory contextFactory, GenericDaoMapper<IRole, RoleDao> mapper, IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Finds all with children asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual async Task<IEnumerable<IRole>> FindAllWithChildrenAsync()
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(await context.Roles.ToListAsync()).ToList();
            }
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override async Task<IRole> SaveOrUpdateAsync(IRole entity)
        {
            IRole role = await base.SaveOrUpdateAsync(entity);

            using (CellentContext context = new CellentContext())
            {
                RoleDao roleDao = await context.Roles.FirstOrDefaultAsync(d => d.Id == role.Id);
                await context.SaveChangesAsync();
                role = Mapper.Convert(roleDao);
                return role;
            }
        }

        #endregion Methods
    }
}