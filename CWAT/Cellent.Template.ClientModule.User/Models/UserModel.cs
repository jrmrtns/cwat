using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Common.Validation;
using Cellent.Template.Common.Validation.Validators;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cellent.Template.Common.ServiceClient;
using ValidationException = Cellent.Template.Common.Exceptions.ValidationException;
using Validator = Cellent.Template.Common.Validation.Validator;

namespace Cellent.Template.ClientModule.User.Models
{
    [MetadataType(typeof(UserModelMetadata))]
    partial class UserModel : ICollectionObserver
    {
        #region Properties

        /// <summary>
        /// Gets or sets Roles
        /// </summary>
        [NotNullValidator]
        public virtual IRoleModel Role { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<IUserModel> SaveAsync()
        {
            ValidationSummary summary = Validator.Validate(this);
            if (!summary.IsValid)
                throw new ValidationException(summary);

            UserDto user = await ServiceClient<IUserService>.ExecuteAsync(d => d.SaveUserAsync(Factory.Convert(this)));

            return Factory.Convert(user);
        }

        #endregion Methods
    }
}