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
    /// DomainEntity for DomainObject
    /// </summary>
    public partial class DomainObject : BaseEntity<IDomainObject>, IDomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObject"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="repository">The user repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public DomainObject(IUnityContainer container, IBaseRepository<IDomainObject> repository, IEventAggregator eventAggregator)
            :base(container, repository, eventAggregator)
        {            
            OnConstruct();
        }

        partial void OnConstruct();

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>
        /// The Type.
        /// </value>
        public virtual String Type {get; set;}

        /// <summary>
        /// Gets or sets the Assembly.
        /// </summary>
        /// <value>
        /// The Assembly.
        /// </value>
        public virtual String Assembly {get; set;}

        /// <summary>
        /// Gets or sets the EntityType.
        /// </summary>
        /// <value>
        /// The EntityType.
        /// </value>
        public virtual String EntityType {get; set;}

        /// <summary>
        /// Gets or sets the EntityAssembly.
        /// </summary>
        /// <value>
        /// The EntityAssembly.
        /// </value>
        public virtual String EntityAssembly {get; set;}

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        /// <value>
        /// The DisplayName.
        /// </value>
        public virtual String DisplayName {get; set;}

    }
}

#endregion
