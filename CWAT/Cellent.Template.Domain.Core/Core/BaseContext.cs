using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Domain.Core.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using ChangeLogDao = Cellent.Template.Domain.Core.Entities.ChangeLogDao;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    /// Der Context für EF
    /// </summary>
    public class BaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContext"/> class.
        /// </summary>
        public BaseContext(string name)
            : base(name)
        {
#if DEBUG
            Database.Log = log => Debug.WriteLine(log);
#endif

            Configuration.LazyLoadingEnabled = false;
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.ObjectMaterialized += (o, args) =>
            {
                BaseDao dao = args.Entity as BaseDao;
                if (dao == null)
                    return;

                dao.State = Constants.EntityState.Unchanged;
            };
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            CreateNewIds();
            CreateChangeLog();

            int count = base.SaveChanges();
            ResetState();
            return count;
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CreateNewIds();
            CreateChangeLog();

            int count = await base.SaveChangesAsync(cancellationToken);
            ResetState();
            return count;
        }

        private void ResetState()
        {
            foreach (var entry in ChangeTracker.Entries().Where(d => d.State != EntityState.Added || d.State != EntityState.Modified))
            {
                BaseDao baseDao = entry.Entity as BaseDao;
                if (baseDao != null)
                    baseDao.State = Constants.EntityState.Unchanged;
            }
        }

        private void CreateChangeLog()
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            var changeSet = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added | EntityState.Deleted);

            foreach (ObjectStateEntry objectStateEntry in changeSet)
            {
                BaseDao baseDao = objectStateEntry.Entity as BaseDao;
                if (baseDao == null || baseDao.ChangeLog == null)
                    continue;

                foreach (UnitOfWorkItem unitOfWorkItem in baseDao.ChangeLog)
                {
                    string typeName = baseDao.GetType().FullName;
                    var domainObject = DomainObjects.First(d => d.EntityType == typeName);

                    ChangeLog.Add(new ChangeLogDao
                    {
                        Id = Guid.NewGuid(),
                        DomainObject = domainObject,
                        ObjectId = baseDao.Id,
                        OldValue = unitOfWorkItem.OldVaue == null ? null : unitOfWorkItem.OldVaue.ToString(),
                        NewValue = unitOfWorkItem.NewValue == null ? null : unitOfWorkItem.NewValue.ToString(),
                        Property = unitOfWorkItem.PropertyName,
                        ChangedAt = unitOfWorkItem.TimeStamp,
                        ChangedBy = CurrentUserId,
                        CreatedAt = unitOfWorkItem.TimeStamp,
                        CreatedBy = CurrentUserId
                    });
                }
            }
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
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

        /// <summary>
        /// Gets or sets the change log.
        /// </summary>
        /// <value>
        /// The change log.
        /// </value>
        public DbSet<ChangeLogDao> ChangeLog { get; set; }

        /// <summary>
        /// Gets or sets the domain objects.
        /// </summary>
        /// <value>
        /// The domain objects.
        /// </value>
        public DbSet<DomainObjectDao> DomainObjects { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ChangeLogDao.CreateTable(modelBuilder);
            DomainObjectDao.CreateTable(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CreateNewIds()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                BaseDao baseDao = entry.Entity as BaseDao;
                if (baseDao != null && baseDao.Id == Guid.Empty)
                {
                    baseDao.Id = Guid.NewGuid();

                    baseDao.CreatedAt = DateTime.Now;
                    baseDao.CreatedBy = CurrentUserId;
                }
                baseDao.ChangedAt = DateTime.Now;
                baseDao.ChangedBy = CurrentUserId;
            }
        }
    }
}