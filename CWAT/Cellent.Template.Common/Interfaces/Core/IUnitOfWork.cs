using System.Collections.Generic;
using Cellent.Template.Common.Interceptors.Helper;

namespace Cellent.Template.Common.Interfaces.Core
{
    /// <summary>
    /// Interface für UnitOfWork Implementation
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        IEnumerable<UnitOfWorkItem> ChangeLog { get; set; }

        /// <summary>
        /// Adds the change.
        /// </summary>
        /// <param name="unitOfWorkItem">The unit of work item.</param>
        void AddChange(UnitOfWorkItem unitOfWorkItem);
    }
}
