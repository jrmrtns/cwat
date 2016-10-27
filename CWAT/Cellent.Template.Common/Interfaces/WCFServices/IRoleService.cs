using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Common.Interfaces.WCFServices
{
    /// <summary>
    /// WcfService interface for Role
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.daimler.com/Cellent")]
    public interface IRoleService
    {
        /// <summary>
        /// Adds a new role or updates existing.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<RoleDto> SaveRoleAsync(RoleDto role);

        /// <summary>
        /// Finds the users.
        /// </summary>
        /// <returns>IEnumerable of the UsersDto</returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<IEnumerable<RoleDto>> FindRolesAsync();
    }
}