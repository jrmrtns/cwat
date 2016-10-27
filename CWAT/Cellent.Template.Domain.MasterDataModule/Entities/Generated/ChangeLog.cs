//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region T4 generated code

using System;
using Cellent.Template.Domain.Core.Implementations.Entities;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cellent.Template.Domain.MasterDataModule.Entities
{
    /// <summary>
    /// DomainEntity for ChangeLog
    /// </summary>
    public partial class ChangeLog : BaseEntity<IChangeLog>, IChangeLog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeLog"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="repository">The user repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ChangeLog(IUnityContainer container, IBaseRepository<IChangeLog> repository, IEventAggregator eventAggregator)
            :base(container, repository, eventAggregator)
        {            
            OnConstruct();
        }

        partial void OnConstruct();

        /// <summary>
        /// Gets or sets the ObjectId.
        /// </summary>
        /// <value>
        /// The ObjectId.
        /// </value>
        public virtual Guid? ObjectId {get; set;}

        /// <summary>
        /// Gets or sets the OldValue.
        /// </summary>
        /// <value>
        /// The OldValue.
        /// </value>
        public virtual String OldValue {get; set;}

        /// <summary>
        /// Gets or sets the NewValue.
        /// </summary>
        /// <value>
        /// The NewValue.
        /// </value>
        public virtual String NewValue {get; set;}

        /// <summary>
        /// Gets or sets the Property.
        /// </summary>
        /// <value>
        /// The Property.
        /// </value>
        public virtual String Property {get; set;}

    }
}

#endregion