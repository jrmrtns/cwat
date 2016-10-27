using System;
using Cellent.Template.Common.DataTransferObjects;

namespace Cellent.Template.Common.Interfaces.Core
{
    /// <summary>
    /// Basisinterface für alle Models
    /// </summary>
    public interface IBaseModel
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
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        Constants.Constants.EntityState State { get; }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        void MapBaseFields(BaseDto source);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }
    }
}
