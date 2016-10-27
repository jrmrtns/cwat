using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Interfaces.Entities
{
    /// <summary>
    /// User DomainEntity
    /// </summary>
    public partial interface IUser
    {
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        IRole Role { get; set; }

        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        IEnumerable<IRight> Rights { get; }

        /// <summary>
        /// Finds all users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IUser>> FindAllAsync();

        /// <summary>
        /// Finds the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IUser> FindById(Guid id);
    }
}