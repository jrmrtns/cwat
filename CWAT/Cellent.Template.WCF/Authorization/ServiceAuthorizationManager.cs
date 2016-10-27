using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;

namespace Cellent.Template.WCF.Authorization
{
    /// <summary>
    /// Prüft, ob der Aufrufer für die Aktion authorisiert ist
    /// </summary>
    public class CellentServiceAuthorizationManager : ServiceAuthorizationManager
    {
        /// <summary>
        /// Checks authorization for the given operation context based on default policy evaluation.
        /// </summary>
        /// <param name="operationContext">The <see cref="T:System.ServiceModel.OperationContext" /> for the current authorization request.</param>
        /// <returns>
        /// true if access is granted; otherwise, false. The default is true.
        /// </returns>
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;

            ReadOnlyCollection<ClaimSet> claims = ServiceSecurityContext.Current.AuthorizationContext.ClaimSets;
            IEnumerable<Claim> allowedOperations = claims
                .Where(d => d.Issuer == ClaimSet.System)
                .SelectMany(d => d.FindClaims(Common.Constants.ClaimTypes.AllowedOperations, Rights.PossessProperty));

            return (UserId != null && allowedOperations.Any(d => (d.Resource.ToString() == action)));
        }

        /// <summary>
        /// Gibt die Id des Users zurück
        /// </summary>
        /// <returns>die Id des angemeldeten Users</returns>
        public static String UserId
        {
            get
            {
                AuthorizationContext context = ServiceSecurityContext.Current.AuthorizationContext;
                return context.ClaimSets
                    .Where(cs => cs.Issuer == ClaimSet.System)
                    .SelectMany(claimSet => claimSet.FindClaims(ClaimTypes.NameIdentifier, Rights.PossessProperty))
                    .Select(claim => claim.Resource.ToString())
                    .FirstOrDefault();
            }
        }
    }
}