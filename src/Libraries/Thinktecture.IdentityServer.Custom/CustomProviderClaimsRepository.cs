using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Thinktecture.IdentityServer.Repositories;

namespace Thinktecture.IdentityServer.Custom
{
    public class CustomProviderClaimsRepository : IClaimsRepository
    {
        public IEnumerable<Claim> GetClaims(ClaimsPrincipal principal, TokenService.RequestDetails requestDetails)
        {
            var username = principal.Identity.Name;
            var claims = new List<Claim>(from c in principal.Claims select c);

            if (!String.IsNullOrEmpty(username))
            {
                claims.Add(new Claim(ClaimTypes.Email, username));
            }

            return claims;
        }

        public IEnumerable<string> GetSupportedClaimTypes()
        {
            throw new NotImplementedException();
        }
    }
}