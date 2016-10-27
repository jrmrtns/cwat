using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Common.ServiceClient;
using Cellent.Template.Common.Validation;
using System.Threading.Tasks;

namespace Cellent.Template.ClientModule.Resource.Models
{
    public partial class ResourceModel
    {
        /// <summary>
        /// Saves the Resource asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Cellent.Template.Common.Exceptions.ValidationException"></exception>
        public async Task<IResourceModel> SaveAsync()
        {
            if (State == Constants.EntityState.Unchanged)
                return this;

            ValidationSummary summary = Validator.Validate(this);
            if (!summary.IsValid)
                throw new ValidationException(summary);

            ResourceDto resource = await ServiceClient<IResourceService>
                .ExecuteAsync(d => d.SaveAsync(Factory.Convert(this)));

            IResourceModel resourceModel = Factory.Convert(resource);
            return resourceModel;
        }
    }
}