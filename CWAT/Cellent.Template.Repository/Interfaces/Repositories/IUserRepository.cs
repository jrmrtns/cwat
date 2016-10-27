using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Cellent.Template.Domain.Interfaces.Entities;
using System;

namespace Cellent.Template.Repository.Interfaces.Repositories
{
    /// <summary>
    /// UserRepository
    /// </summary>
    [Log]
    public interface IUserRepository : IBaseRepository<IUser>
    {
        /// <summary>
        /// Finds user by name
        /// </summary>
        /// <param name="name">the name</param>
        /// <returns>IUser</returns>
        IUser FindByName(string name);

        /// <summary>
        /// Finds user by Id
        /// </summary>
        IUser FindById(Guid id);
    }
}