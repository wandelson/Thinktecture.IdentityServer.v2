using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Security;
using Thinktecture.IdentityServer.Custom.Helpers;
using Thinktecture.IdentityServer.Repositories;

namespace Thinktecture.IdentityServer.Custom
{
    public class CustomUserRepository : ICustomUserRepository
    {
        public bool ValidateUser(string username, string password)
        {
            if (username.Contains("@"))
            {
                using (var context = new PortalContext())
                {
                    var user = context.User.FirstOrDefault(p => p.Mail == username);

                    if (user == null)
                    {
                        return false;
                    }

                    var pwdManager = new PasswordManagerHelper();

                    var passwordSalt = UtilityHelper.GetString(user.PasswordSalt);
                    var passwordHash = UtilityHelper.GetString(user.PasswordHash);

                    var result = pwdManager.IsPasswordMatch(password, passwordSalt, passwordHash);

                    return result;
                }
            }

            return Membership.ValidateUser(username, password);
        }

        public IEnumerable<string> GetRoles(string username)
        {
            throw new System.NotImplementedException();
        }


        public bool ValidateUser(X509Certificate2 clientCertificate, out string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}