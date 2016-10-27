using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;

namespace Cellent.Template.Domain.UserModule.DomainMapper
{
    /// <summary>
    /// Konvertiert die User
    /// </summary>
    partial class UserMapper
    {
        private readonly GenericDomainMapper<IRole, RoleDto> _roleMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="roleMapper">The role mapper.</param>
        public UserMapper(IDomainFactory<IUser> factory, GenericDomainMapper<IRole, RoleDto> roleMapper)
            : this(factory)
        {
            _roleMapper = roleMapper;
        }

        partial void OnConvertAdditionalFields(IUser source, UserDto dest)
        {
            dest.Role = _roleMapper.Convert(source.Role);
        }

        partial void OnConvertAdditionalFields(UserDto source, IUser dest)
        {
            dest.Role = _roleMapper.Convert(source.Role);
        }
    }
}