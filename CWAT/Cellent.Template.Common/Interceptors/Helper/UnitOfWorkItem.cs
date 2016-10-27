using System;

namespace Cellent.Template.Common.Interceptors.Helper
{
    /// <summary>
    /// Entry in ChangeLogList
    /// </summary>
    public class UnitOfWorkItem
    {
        /// <summary>
        /// Gets or sets the old vaue.
        /// </summary>
        /// <value>
        /// The old vaue.
        /// </value>
        public object OldVaue { get; set; }
        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        /// <value>
        /// The new value.
        /// </value>
        public object NewValue { get; set; }
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }
        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>
        /// The object identifier.
        /// </value>
        public Guid ObjectId { get; set; }
        /// <summary>
        /// Gets or sets the domain object identifier.
        /// </summary>
        /// <value>
        /// The domain object identifier.
        /// </value>
        public string DomainObject { get; set; }
        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        public Guid ChangedBy { get; set; }
    }
}
