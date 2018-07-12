using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Common.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cellent.Template.Common.ServiceClient;

namespace Cellent.Template.ClientModule.User.Models
{
    public partial class RoleModel
    {
        /// <summary>
        /// Gets or sets the right groups.
        /// </summary>
        /// <value>
        /// The right groups.
        /// </value>
        public virtual IEnumerable<IRightModel> Rights { get; set; }

        /// <summary>
        /// Marks entity state as modified
        /// </summary>
        public virtual void MarkModified()
        {
            ChangedAt = DateTime.Now;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IRoleModel> SaveAsync()
        {
            ValidationSummary summary = Validator.Validate(this);
            if (!summary.IsValid)
                throw new ValidationException(summary);

            RoleDto role = await ServiceClient<IRoleService>.ExecuteAsync(d => d.SaveRoleAsync(Factory.Convert(this)));

            IRoleModel roleModel = Factory.Convert(role);

            return roleModel;
        }
    }
}