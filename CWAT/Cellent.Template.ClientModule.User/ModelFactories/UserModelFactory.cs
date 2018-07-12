using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.DataTransferObjects;
using Prism.Events;

namespace Cellent.Template.ClientModule.User.ModelFactories
{
    public partial class UserModelFactory
    {
        private GenericFactory<IRoleModel, RoleDto> _roleFactory;

        /// <summary>
        /// RoleModelFactory
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="roleFactory"></param>
        public UserModelFactory(IEventAggregator eventAggregator, GenericFactory<IRoleModel, RoleDto> roleFactory)
            : this(eventAggregator)
        {
            _roleFactory = roleFactory;
        }

        partial void OnConvertAdditionalFields(IUserModel source, UserDto dest)
        {
            dest.Role = _roleFactory.Convert(source.Role);
        }

        partial void OnConvertAdditionalFields(UserDto source, IUserModel dest)
        {
            dest.Role = _roleFactory.Convert(source.Role);
        }
    }
}