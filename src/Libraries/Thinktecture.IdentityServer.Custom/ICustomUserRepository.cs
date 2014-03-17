using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Thinktecture.IdentityServer.Custom
{
    public interface ICustomUserRepository
    {
        bool ValidateUser(string username, string password);

        IEnumerable<string> GetRoles(string username);

        bool ValidateUser(X509Certificate2 clientCertificate, out string userName);
    }
}