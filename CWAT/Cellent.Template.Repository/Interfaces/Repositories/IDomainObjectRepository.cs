using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using System;

namespace Cellent.Template.Repository.Interfaces.Repositories
{
    /// <summary>
    /// IDomainObjectRepository
    /// </summary>
    public interface IDomainObjectRepository : IBaseRepository<IDomainObject>
    {
        /// <summary>
        /// Finds the name of the domainObject by DAO type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        Guid FindByDaoTypeName(string typeName);

        /// <summary>
        /// Finds the name of the by domain type.
        /// </summary>
        /// <param name="typeName">The typename.</param>
        /// <returns></returns>
        IDomainObject FindByDomainTypeName(string typeName);

        /// <summary>
        /// Finds the name of the identifier by domain type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        Guid FindIdByDomainTypeName(string typeName);
    }
}