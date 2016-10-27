using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Events;
using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Entities;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Repository.Context;
using Prism.Events;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Repositories
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <seealso cref="BaseRepository{IChangeLog, ChangeLogDao}" />
    public class ChangeLogRepository : BaseRepository<IChangeLog, ChangeLogDao>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObjectRepository" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ChangeLogRepository(IContextFactory contextFactory, GenericDaoMapper<IChangeLog, ChangeLogDao> mapper,
            IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Saves the or update asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override async Task<IChangeLog> SaveOrUpdateAsync(IChangeLog entity)
        {
            ChangeLogDao dao = Mapper.Convert(entity);

            dao.ChangedAt = DateTime.Now;
            if (dao.ChangedBy == Guid.Empty)
                dao.ChangedBy = CurrentUserId;

            using (CellentContext context = new CellentContext())
            {
                switch (dao.State)
                {
                    case Constants.EntityState.Created:
                        dao.CreatedAt = DateTime.Now;
                        if (dao.CreatedBy == Guid.Empty)
                            dao.CreatedBy = CurrentUserId;
                        context.Set(dao.GetType()).Add(dao);
                        break;

                    case Constants.EntityState.Modified:
                        context.Set(dao.GetType()).Attach(dao);
                        context.Entry(dao).State = EntityState.Modified;
                        break;
                }

                context.Entry(dao.DomainObject).State = EntityState.Unchanged;
                await context.SaveChangesAsync();

                entity = Mapper.Convert(dao);
                EventAggregator.GetEvent<EntityUpdated>().Publish(entity);

                return entity;
            }
        }

        #endregion Methods
    }
}