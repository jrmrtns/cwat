using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Entities;
using System;

namespace Cellent.Template.Domain.UserModule.DaoMapper
{
    /// <summary>
    /// Mapps the IUser to UserDao and vice versa
    /// </summary>
    partial class UserMapper
    {
        private readonly GenericDaoMapper<IRole, RoleDao> _roleMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="roleMapper">The role mapper.</param>
        public UserMapper(Lazy<IDomainFactory<IUser>> factory, RoleMapper roleMapper)
            : this(factory)
        {
            _roleMapper = roleMapper;
        }

        /// <summary>
        /// Gets the role mapper.
        /// </summary>
        /// <value>
        /// The role mapper.
        /// </value>
        public GenericDaoMapper<IRole, RoleDao> RoleMapper
        {
            get { return _roleMapper; }
        }

        partial void OnConvertAdditionalFields(IUser source, UserDao dest)
        {
            dest.Role = RoleMapper.Convert(source.Role);
        }

        partial void OnConvertAdditionalFields(UserDao source, IUser dest)
        {
            dest.Role = RoleMapper.Convert(source.Role);
        }
    }
}