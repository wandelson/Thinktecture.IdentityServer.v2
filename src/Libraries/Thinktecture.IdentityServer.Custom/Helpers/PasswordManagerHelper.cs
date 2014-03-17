namespace Thinktecture.IdentityServer.Custom.Helpers
{
    public class PasswordManagerHelper
    {
        private HashComputerHelper m_hashComputer = new HashComputerHelper();

        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = SaltGeneratorHelper.GetSaltString();

            string finalString = plainTextPassword + salt;

            return m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        }
    }
}