using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Entities;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Repository.Context;
using Cellent.Template.Repository.Interfaces.Repositories;
using Prism.Events;
using System;
using System.Linq;

namespace Cellent.Template.Repository.Repositories
{
    /// <summary>
    /// DomainObjectRepository
    /// </summary>
    public class DomainObjectRepository : BaseRepository<IDomainObject, DomainObjectDao>, IDomainObjectRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObjectRepository" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public DomainObjectRepository(IContextFactory contextFactory, GenericDaoMapper<IDomainObject, DomainObjectDao> mapper,
            IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Finds the name of the by DAO type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [Cache(TypeToMonitor = typeof(IDomainObject))]
        public virtual Guid FindByDaoTypeName(string typeName)
        {
            using (CellentContext context = new CellentContext())
            {
                return context.DomainObjects.First(d => d.EntityType == typeName).Id;
            }
        }

        /// <summary>
        /// Finds the name of the by domain type.
        /// </summary>
        /// <param name="typeName">The type.</param>
        /// <returns></returns>
        [Cache(TypeToMonitor = typeof(IDomainObject))]
        public virtual IDomainObject FindByDomainTypeName(string typeName)
        {
            using (CellentContext context = new CellentContext())
            {
                return Mapper.Convert(context.DomainObjects.First(d => d.Type == typeName));
            }
        }

        /// <summary>
        /// Finds the name of the by DAO type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [Cache(TypeToMonitor = typeof(IDomainObject))]
        public virtual Guid FindIdByDomainTypeName(string typeName)
        {
            using (CellentContext context = new CellentContext())
            {
                return context.DomainObjects.First(d => d.Type == typeName).Id;
            }
        }

        #endregion Methods
    }
}