namespace Thinktecture.IdentityServer.Custom
{
    public partial class CustomConfigurationSection : global::System.Configuration.ConfigurationSection
    {
        public const string SectionName = "custom.repositories";

        internal const global::System.String CustomUserRepositoryName = "customUserManagement";

        [global::System.Configuration.ConfigurationProperty(CustomUserRepositoryName, IsRequired = false, IsKey = false, IsDefaultCollection = false, DefaultValue = "Thinktecture.IdentityServer.Custom.CustomUserRepository, Thinktecture.IdentityServer.Custom")]
        public global::System.String CustomUserRepository
        {
            get
            {
                return (global::System.String)base[CustomUserRepositoryName];
            }
            set
            {
                base[CustomUserRepositoryName] = value;
            }
        }
    }
}