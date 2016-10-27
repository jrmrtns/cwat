namespace Cellent.Template.Domain.Core.Interfaces.Entities
{
    public partial interface IChangeLog
    {
        /// <summary>
        /// Gets or sets the domain object.
        /// </summary>
        /// <value>
        /// The domain object.
        /// </value>
        IDomainObject DomainObject { get; set; }
    }
}