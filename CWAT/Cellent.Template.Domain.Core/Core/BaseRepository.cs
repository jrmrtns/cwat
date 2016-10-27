using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Events;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    /// Basisklasse für Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public abstract class BaseRepository<T, TK> : IBaseRepository<T>
        where TK : BaseDao
        where T : IEntity<T>
    {
        #region Fields

        private readonly IContextFactory _contextFactory;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T, TK}" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected BaseRepository(IContextFactory contextFactory, GenericDaoMapper<T, TK> mapper, IEventAggregator eventAggregator)
        {
            _contextFactory = contextFactory;
            Mapper = mapper;
            EventAggregator = eventAggregator;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public GenericDaoMapper<T, TK> Mapper { get; }

        /// <summary>
        /// gibt den aktuellen UserNamen zurück
        /// </summary>
        /// <returns></returns>
        protected static Guid CurrentUserId
        {
            get
            {
                if ((ServiceSecurityContext.Current == null) || (ServiceSecurityContext.Current.AuthorizationContext == null))
                    return Guid.Empty;

                AuthorizationContext context = ServiceSecurityContext.Current.AuthorizationContext;
                object result = context
                    .ClaimSets
                    .Where(cs => cs.Issuer == ClaimSet.System)
                    .SelectMany(d => d.FindClaims(System.IdentityModel.Claims.ClaimTypes.NameIdentifier, Rights.PossessProperty))
                    .Select(claim => claim.Resource)
                    .FirstOrDefault();

                return result == null ? Guid.Empty : (Guid)result;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Deletes the entity async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            using (BaseContext context = _contextFactory.Create())
            {
                TK dao = Mapper.Convert(entity);
                context.Entry(dao).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
            return true;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> FindAll()
        {
            using (BaseContext context = _contextFactory.Create())
            {
                return Mapper.Convert(context.Set<TK>().ToList()).ToList();
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="orderBy">The order.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindAll(String orderBy, int page, int pageSize)
        {
            using (BaseContext context = _contextFactory.Create())
            {
                return Mapper.Convert(context.Set<TK>()
                    .OrderBy(orderBy)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList()).ToList();
            }
        }

        /// <summary>
        /// Selects all data.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            using (BaseContext context = _contextFactory.Create())
            {
                return Mapper.Convert(await context.Set<TK>().ToListAsync()).ToList();
            }
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(Guid id)
        {
            using (BaseContext context = _contextFactory.Create())
            {
                return Mapper.Convert(await context.Set<TK>().FindAsync(id));
            }
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> SaveOrUpdateAsync(T entity)
        {
            TK dao = Mapper.Convert(entity);

            dao.ChangedAt = DateTime.Now;
            dao.ChangedBy = CurrentUserId;

            using (BaseContext context = _contextFactory.Create())
            {
                switch (dao.State)
                {
                    case Constants.EntityState.Created:
                        dao.CreatedAt = DateTime.Now;
                        dao.CreatedBy = CurrentUserId;
                        context.Set(dao.GetType()).Add(dao);
                        break;

                    case Constants.EntityState.Modified:
                        context.Set(dao.GetType()).Attach(dao);
                        context.Entry(dao).State = EntityState.Modified;
                        break;
                }

                await context.SaveChangesAsync();

                entity = Mapper.Convert(dao);
                EventAggregator.GetEvent<EntityUpdated>().Publish(entity);

                return entity;
            }
        }

        #endregion Methods
    }
}