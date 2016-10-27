using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;
using Cellent.Template.Common.Interfaces.Core;

namespace Cellent.Template.Domain.Core.Implementations
{
    /// <summary>
    /// Extension
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Retrieves the current user identifier.
        /// </summary>
        /// <param name="baseDao">The base DAO.</param>
        /// <returns></returns>
        public static Guid RetrieveCurrentUserId(this IBaseEntity baseDao)
        {
            if ((ServiceSecurityContext.Current == null) || (ServiceSecurityContext.Current.AuthorizationContext == null))
                return Guid.Empty;

            AuthorizationContext context = ServiceSecurityContext.Current.AuthorizationContext;
            object result = context
                .ClaimSets
                .Where(cs => cs.Issuer == ClaimSet.System)
                .SelectMany(d => d.FindClaims(ClaimTypes.NameIdentifier, Rights.PossessProperty))
                .Select(claim => claim.Resource)
                .FirstOrDefault();

            return result == null ? Guid.Empty : (Guid)result;
        }

    }
}
