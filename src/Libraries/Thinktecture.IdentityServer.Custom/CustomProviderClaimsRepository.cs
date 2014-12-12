using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web.Profile;
using Thinktecture.IdentityServer.Repositories;

namespace Thinktecture.IdentityServer.Custom
{
    public class CustomProviderClaimsRepository : IClaimsRepository
    {
        private const string ProfileClaimPrefix = "http://identityserver.thinktecture.com/claims/profileclaims/";

        public IEnumerable<Claim> GetClaims(ClaimsPrincipal principal, TokenService.RequestDetails requestDetails)
        {
            var username = principal.Identity.Name;
            var claims = new List<Claim>(from c in principal.Claims select c);

            if (!String.IsNullOrEmpty(username))
            {
                using (var context = new PortalEntities())
                {
                    var user = context.User.FirstOrDefault(p => p.Mail == username);

                    if (user != null)
                    {
                        claims.Add(new Claim("IsAdmin", user.IsAdmin.ToString()));
                    }
                }

                claims.Add(new Claim(ClaimTypes.Email, username));
            }

            return claims;
        }

        public IEnumerable<string> GetSupportedClaimTypes()
        {
            var claimTypes = new List<string>
            {
                ClaimTypes.Name,
                ClaimTypes.Email,
                ClaimTypes.Role
            };

            if (ProfileManager.Enabled)
            {
                foreach (SettingsProperty prop in ProfileBase.Properties)
                {
                    claimTypes.Add(GetProfileClaimType(prop.Name.ToLowerInvariant()));
                }
            }

            return claimTypes;
        }

        protected virtual string GetProfileClaimType(string propertyName)
        {
            if (StandardClaimTypes.Mappings.ContainsKey(propertyName))
            {
                return StandardClaimTypes.Mappings[propertyName];
            }
            else
            {
                return string.Format("{0}{1}", ProfileClaimPrefix, propertyName);
            }
        }
    }
}