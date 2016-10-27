using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core.Interfaces;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Prism.Events;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    /// DefaultRepository
    /// </summary>
    /// <typeparam name="T">Type of IEntity</typeparam>
    /// <typeparam name="TK">The type of the Dao.</typeparam>
    [Log]
    public class DefaultRepository<T, TK> : BaseRepository<T, TK> where TK : BaseDao
        where T : IEntity<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRepository{T, TK}" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator">The EventAggregator</param>
        public DefaultRepository(IContextFactory contextFactory, GenericDaoMapper<T, TK> mapper, IEventAggregator eventAggregator)
            : base(contextFactory, mapper, eventAggregator)
        {
        }
    }
}