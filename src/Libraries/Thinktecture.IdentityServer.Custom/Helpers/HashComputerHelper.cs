using System.Security.Cryptography;

namespace Thinktecture.IdentityServer.Custom.Helpers
{
    public class HashComputerHelper
    {
        public string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to
            // generate the hash from this salted password
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] dataBytes = UtilityHelper.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return UtilityHelper.GetString(resultBytes);
        }
    }
}