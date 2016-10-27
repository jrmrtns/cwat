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
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Validation.Validators;
using Cellent.Template.Domain.Core.Interfaces.Entities;

namespace Cellent.Template.Domain.Interfaces.Entities
{
    /// <summary>
    /// Interface für Right
    /// </summary>
    [Log]
    public partial interface IRight : IEntity<IRight>
    {

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        [NotNullValidator]
        [StringLengthValidator(MaxLen = 100)]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        [StringLengthValidator(MaxLen = 100)]
        String Description { get; set; }

        /// <summary>
        /// Gets or sets the Claim.
        /// </summary>
        /// <value>
        /// The Claim.
        /// </value>
        [NotNullValidator]
        [StringLengthValidator(MaxLen = 100)]
        String Claim { get; set; }

        /// <summary>
        /// Gets or sets the Resource.
        /// </summary>
        /// <value>
        /// The Resource.
        /// </value>
        [NotNullValidator]
        [StringLengthValidator(MaxLen = 100)]
        String Resource { get; set; }
    }
}

#endregion