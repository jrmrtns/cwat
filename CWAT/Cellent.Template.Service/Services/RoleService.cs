using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Service.Services
{
    /// <summary>
    /// Implementation for MasterDataService
    /// </summary>
    [Log]
    public class RoleService : IRoleService
    {
        private readonly IDomainFactory<IRole> _roleFactory;
        private readonly GenericDomainMapper<IRole, RoleDto> _roleMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService" /> class.
        /// </summary>
        /// <param name="roleMapper">The role mapper.</param>
        /// <param name="roleFactory">The role factory.</param>
        public RoleService(GenericDomainMapper<IRole, RoleDto> roleMapper, IDomainFactory<IRole> roleFactory)
        {
            _roleMapper = roleMapper;
            _roleFactory = roleFactory;
        }

        /// <summary>
        /// Finds the roles asynchronous.
        /// </summary>
        /// <returns>IEnumerable of the RoleDto</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public virtual async Task<IEnumerable<RoleDto>> FindRolesAsync()
        {
            IRole role = _roleFactory.Create();
            return _roleMapper.Convert(await role.FindAllWithChildrenAsync());
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="roleDto">The role.</param>
        /// <returns></returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public virtual async Task<RoleDto> SaveRoleAsync(RoleDto roleDto)
        {
            IRole role = _roleMapper.Convert(roleDto);
            return _roleMapper.Convert(await role.SaveOrUpdateAsync(role));
        }
    }
}