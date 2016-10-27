using System;

namespace Cellent.Template.Common.Interfaces.Core
{
    /// <summary>
    /// Basis für Daos
    /// </summary>
    public interface IBaseDao
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the changed at.
        /// </summary>
        /// <value>
        /// The changed at.
        /// </value>
        DateTime ChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        Guid ChangedBy { get; set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        Constants.Constants.EntityState State { get; }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        void MapBaseFields(IBaseEntity source);
    }
}