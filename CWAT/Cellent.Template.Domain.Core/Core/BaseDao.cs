using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cellent.Template.Domain.Core
{
    /// <summary>
    /// Basisklasse für alle DAOs
    /// </summary>
    public class BaseDao : IBaseDao
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDao"/> class.
        /// </summary>
        public BaseDao()
        {
            State = Constants.EntityState.Created;
        }

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
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [NotMapped]
        public Constants.EntityState State { get; set; }

        /// <summary>
        /// Gets or sets the change log.
        /// </summary>
        /// <value>
        /// The change log.
        /// </value>
        [NotMapped]
        public IEnumerable<UnitOfWorkItem> ChangeLog { get; set; }

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
    }
}