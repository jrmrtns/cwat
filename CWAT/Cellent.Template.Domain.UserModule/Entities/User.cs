using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.UserModule.Entities
{
    /// <summary>
    /// Domainobject User
    /// </summary>
    public partial class User
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        public IEnumerable<IRight> Rights
        {
            get
            {
                return Role.Rights;
            }
        }

        /// <summary>
        /// Finds all users asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IUser>> FindAllAsync()
        {
            return await ((IUserRepository)Repository).FindAllAsync();
        }

        /// <summary>
        /// Finds the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IUser> FindById(Guid id)
        {
            return await Task.Factory.StartNew(() => ((IUserRepository)Repository).FindById(id));
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IRole Role { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Saves the or update the entity asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override async Task<IUser> SaveOrUpdateAsync(IUser entity)
        {
            IUser user = await base.SaveOrUpdateAsync(entity);
            return user;
        }

        #endregion Public Methods
    }
}