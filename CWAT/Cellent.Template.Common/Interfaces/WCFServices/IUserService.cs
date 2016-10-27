using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Common.Interfaces.WCFServices
{
    /// <summary>
    /// Test WcfService
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.daimler.com/Cellent")]
    public interface IUserService
    {
        /// <summary>
        /// Finds the users.
        /// </summary>
        /// <returns>
        /// IEnumerable of the UsersDto
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<IEnumerable<UserDto>> FindUsersAsync();

        /// <summary>
        /// Adds a new user or updates existing.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<UserDto> SaveUserAsync(UserDto user);

        /// <summary>
        /// Gets the current user asynchronous.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<Tuple<UserDto, IEnumerable<ClientRights>>> GetCurrentUserAsync();
    }
}