using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Events;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Context;
using Cellent.Template.Repository.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Cellent.Template.Repository.Repositories
{
    /// <summary>
    /// UserRepository
    /// </summary>
    [Log]
    public class UserRepository : BaseRepository<IUser, UserDao>, IUserRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The EventAggregator</param>
        public UserRepository(IContextFactory contextFactory, GenericDaoMapper<IUser, UserDao> mapper, IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        { }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Selects all data.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<IUser>> FindAllAsync()
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(await context.Users.Include(d => d.Role.Rights).ToListAsync());
            }
        }

        /// <summary>
        /// Finds user by Id
        /// </summary>
        [Cache(TypeToMonitor = typeof(IUser))]
        public virtual IUser FindById(Guid id)
        {
            using (CellentContext context = new CellentContext())
            {
                return
                    Mapper.Convert(context.Users.Include(d => d.Role.Rights)
                        .FirstOrDefault(d => d.Id == id));
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        [Cache(TypeToMonitor = typeof(IUser))]
        public virtual IUser FindByName(string name)
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(context.Users
                    .Include(d => d.Role.Rights)
                    .FirstOrDefault(d => d.Name.ToLower() == name.ToLower()));
            }
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="entity">The IUser entity.</param>
        /// <returns></returns>
        public override async Task<IUser> SaveOrUpdateAsync(IUser entity)
        {
            UserDao dao = Mapper.Convert(entity);
            dao.RoleId = dao.Role.Id;

            dao.ChangedAt = DateTime.Now;
            if (dao.ChangedBy == Guid.Empty)
                dao.ChangedBy = CurrentUserId;

            using (CellentContext context = new CellentContext())
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

                context.Entry(dao.Role).State = EntityState.Unchanged;
                await context.SaveChangesAsync();

                entity = Mapper.Convert(dao);
                EventAggregator.GetEvent<EntityUpdated>().Publish(entity);

                return entity;
            }
        }

        #endregion Public Methods
    }
}