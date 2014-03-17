using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Security;
using Thinktecture.IdentityServer.Repositories;

namespace Thinktecture.IdentityServer.Custom
{
    public class CustomProviderUserRepository : IUserRepository
    {
        [Import]
        public IClientCertificatesRepository Repository { get; set; }

        [Import]
        public ICustomUserRepository CustomRepository { get; set; }

        public CustomProviderUserRepository()
        {
            Container.Current.SatisfyImportsOnce(this);
        }

        public virtual bool ValidateUser(string userName, string password)
        {
            if (userName.Contains("@"))
            {
                return CustomRepository.ValidateUser(userName, password);
            }

            return Membership.ValidateUser(userName, password);
        }

        public IEnumerable<string> GetRoles(string userName)
        {
            if (Roles.Enabled)
            {
                return Roles.GetRolesForUser(userName)
                            .Where(role => role.StartsWith(Constants.Roles.InternalRolesPrefix))
                            .ToList();
            }

            return new List<string>();
        }



        public bool ValidateUser(X509Certificate2 clientCertificate, out string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}