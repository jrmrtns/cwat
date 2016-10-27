using System;
using System.Collections.Generic;
using System.Linq;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.Core;

namespace Cellent.Template.Common.DataTransferObjects
{
    /// <summary>
    /// Basis für alle DTOs
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the changed at.
        /// </summary>
        /// <value>
        /// The changed at.
        /// </value>
        public DateTime ChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        public Guid ChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the change log.
        /// </summary>
        /// <value>
        /// The change log.
        /// </value>
        public IEnumerable<UnitOfWorkItem> ChangeLog { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public Constants.Constants.EntityState State { get; set; }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        public void MapBaseFields(IBaseEntity source)
        {
            Id = source.Id;
            CreatedAt = source.CreatedAt;
            CreatedBy = source.CreatedBy;
            ChangedAt = source.ChangedAt;
            ChangedBy = source.ChangedBy;

            // ReSharper disable once SuspiciousTypeConversion.Global
            IUnitOfWork unitOfWork = source as IUnitOfWork;
            if (unitOfWork != null)
                ChangeLog = unitOfWork.ChangeLog;

            State = source.State;
        }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        public void MapBaseFields(IBaseModel source)
        {
            Id = source.Id;
            CreatedAt = source.CreatedAt;
            CreatedBy = source.CreatedBy;
            ChangedAt = source.ChangedAt;
            ChangedBy = source.ChangedBy;

            // ReSharper disable once SuspiciousTypeConversion.Global
            IUnitOfWork unitOfWork = source as IUnitOfWork;
            if (unitOfWork != null)
                ChangeLog = unitOfWork.ChangeLog.ToArray();

            State = source.State;
        }
    }
}
