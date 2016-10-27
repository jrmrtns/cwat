using Cellent.Template.Common.Interceptors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Core.Interfaces.Repositories
{
    /// <summary>
    /// Basisrepository, stellt CRUD Operationen bereit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Log]
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> SaveOrUpdateAsync(T entity);

        /// <summary>
        /// Deletes the entity asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Deletes the entity asynchronous.
        /// </summary>
        /// <param name="id">der Schlüssel</param>
        /// <returns></returns>
        Task<T> FindAsync(Guid id);

        /// <summary>
        /// Finds all async.
        /// </summary>
        /// <returns>Result</returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>Result</returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>Result</returns>
        IEnumerable<T> FindAll(String orderBy, int page, int pageSize);
    }
}