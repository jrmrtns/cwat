using Cellent.Template.Common.Interfaces.Core;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Core.Interfaces.Entities
{
    /// <summary>
    /// Domain Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T> : IBaseEntity
    {
        /// <summary>
        /// Saves the or update an entity asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> SaveOrUpdateAsync(T entity);
    }
}