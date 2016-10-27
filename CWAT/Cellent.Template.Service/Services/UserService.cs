using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.WCF.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Service.Services
{
    /// <summary>
    /// Implementierung des IService
    /// </summary>
    [Log]
    public class UserService : IUserService
    {
        #region Fields (2) 

        private readonly IDomainFactory<IUser> _userFactory;
        private readonly GenericDomainMapper<IUser, UserDto> _userMapper;
        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userMapper">The user mapper.</param>
        /// <param name="userFactory">The user factory.</param>
        public UserService(GenericDomainMapper<IUser, UserDto> userMapper, IDomainFactory<IUser> userFactory)
        {
            _userMapper = userMapper;
            _userFactory = userFactory;
        }

        #endregion Constructors 

        #region Properties (3) 

        #endregion Properties 

        /// <summary>
        /// Finds the users.
        /// </summary>
        /// <returns>
        /// IEnumerable of the UsersDto
        /// </returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public virtual async Task<IEnumerable<UserDto>> FindUsersAsync()
        {
            IUser user = _userFactory.Create();
            return _userMapper.Convert(await user.FindAllAsync());
        }

        /// <summary>
        /// Gets the current user asynchronous.
        /// </summary>
        /// <returns></returns>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public virtual async Task<Tuple<UserDto, IEnumerable<ClientRights>>> GetCurrentUserAsync()
        {
            Guid id = new Guid(CellentServiceAuthorizationManager.UserId);
            IUser user = _userFactory.Create();

            user = await user.FindById(id);
            IEnumerable<IRight> rights = user.Rights.Where(d => d.Claim == ClaimTypes.ClientRights);

            var userDto = _userMapper.Convert(user);
            var clientRights = rights.Select(d => (ClientRights)Enum.Parse(typeof(ClientRights), d.Resource));

            return new Tuple<UserDto, IEnumerable<ClientRights>>(userDto, clientRights);
        }

        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>the updated user</returns>
        /// <exception cref="Cellent.Template.Common.Exceptions.ValidationException">thrown if user is not valid</exception>
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public virtual async Task<UserDto> SaveUserAsync(UserDto userDto)
        {
            IUser user = _userMapper.Convert(userDto);
            return _userMapper.Convert(await user.SaveOrUpdateAsync(user));
        }
    }
}