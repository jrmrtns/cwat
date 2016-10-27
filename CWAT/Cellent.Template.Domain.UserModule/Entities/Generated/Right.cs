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
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cellent.Template.Domain.UserModule.Entities
{
    /// <summary>
    /// DomainEntity for Right
    /// </summary>
    public partial class Right : BaseEntity<IRight>, IRight
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Right"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="repository">The user repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Right(IUnityContainer container, IBaseRepository<IRight> repository, IEventAggregator eventAggregator)
            :base(container, repository, eventAggregator)
        {            
            OnConstruct();
        }

        partial void OnConstruct();

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        public virtual String Name {get; set;}

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public virtual String Description {get; set;}

        /// <summary>
        /// Gets or sets the Claim.
        /// </summary>
        /// <value>
        /// The Claim.
        /// </value>
        public virtual String Claim {get; set;}

        /// <summary>
        /// Gets or sets the Resource.
        /// </summary>
        /// <value>
        /// The Resource.
        /// </value>
        public virtual String Resource {get; set;}

    }
}

#endregion