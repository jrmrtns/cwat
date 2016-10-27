using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel;
using Claim = System.IdentityModel.Claims.Claim;

namespace Cellent.Template.WCF.Authorization
{
    /// <summary>
    /// Steuert die Authorisierung für den User
    ///// </summary>
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        #region Constructors (1)

        #region Public Constructors

        #region Public Constructors

        /// <summary>
        /// Erstellt eine neue AuthorizationPolicy
        /// </summary>
        public AuthorizationPolicy()
        {
            Id = Guid.NewGuid().ToString();
        }

        #endregion Public Constructors

        #endregion Public Constructors

        #endregion Constructors

        #region Properties (2)

        #region Public Properties

        #region Public Properties

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container { get; set; }

        /// <summary>
        /// Id der AuthorizationPolicy
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Aussteller des Claimsets
        /// </summary>
        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        #endregion Public Properties

        #endregion Public Properties

        #endregion Properties

        #region Methods (2)

        #region Public Methods (1)

        #region Public Methods

        #region Public Methods

        /// <summary>
        /// Lädt das Claimset der Berechtigungen für den aufrufenden Windows User
        /// </summary>
        /// <param name="evaluationContext"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IIdentity client = GetClientIdentity(evaluationContext);
            IPrincipal principal = new ClaimsPrincipal(client);
            evaluationContext.Properties["Principal"] = principal;

            IUserRepository repository = Container.Resolve<IUserRepository>();
            if (repository != null)
            {
                IUser currentUser = repository.FindByName(principal.Identity.Name);
                if (currentUser != null && !currentUser.Deactivated)
                {
                    IList<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(System.IdentityModel.Claims.ClaimTypes.NameIdentifier,
                        currentUser.Id,
                        Rights.PossessProperty));

                    IEnumerable<IRight> rights = currentUser.Role.Rights;

                    IEnumerable<IRight> distinctRights = rights.Distinct();
                    foreach (IRight right in distinctRights)
                    {
                        claims.Add(new Claim(right.Claim, right.Resource, Rights.PossessProperty));
                    }

                    evaluationContext.AddClaimSet(this, new DefaultClaimSet(ClaimSet.System, claims));
                }
                else if (currentUser == null)
                {
                    string errorMessage = "Unknow User detected";
                    throw new FaultException<RemoteFault>(new RemoteFault(errorMessage, Constants.FaultExceptionEnum.UnknowUser), errorMessage);
                }
                else if (currentUser.Deactivated)
                {
                    string errorMessage = "Current User deactivated";
                    throw new FaultException<RemoteFault>(new RemoteFault(errorMessage, Constants.FaultExceptionEnum.DeactivedUser), errorMessage);
                }
            }
            return true;
        }

        #endregion Public Methods

        #endregion Public Methods

        #endregion Public Methods

        #region Private Methods (1)

        #region Private Methods

        #region Private Methods

        /// <summary>
        /// Gibt die Windows-Identität des Aufrufers zurück
        /// </summary>
        /// <param name="evaluationContext"></param>
        /// <returns></returns>
        private static IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object identityObjects;
            if (!evaluationContext.Properties.TryGetValue("Identities", out identityObjects))
                throw new MissingIdentityException();

            IList<IIdentity> identities = identityObjects as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new MissingIdentityException();

            return identities[0];
        }

        #endregion Private Methods

        #endregion Private Methods

        #endregion Private Methods

        #endregion Methods
    }
}