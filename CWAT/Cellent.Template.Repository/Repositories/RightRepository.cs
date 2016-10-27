using DCX.DPE.Domain.Core.Interfaces.Entities;
using DCX.DPE.Domain.Core.Interfaces.Repositories;
using DCX.DPE.Domain.Repository.Core;
using DCX.DPE.Domain.Repository.Entities;
using Prism.Events;

namespace DCX.DPE.Domain.Repository.Repositories
{
    /// <summary>
    /// RoleRepository
    /// </summary>
    public class RightRepository : BaseRepository<IRight, RightDao>, IRightRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="eventAggregator"></param>
        public RightRepository(GenericDaoMapper<IRight, RightDao> mapper, IEventAggregator eventAggregator) 
            : base(mapper, eventAggregator)
        {}
    }
}
