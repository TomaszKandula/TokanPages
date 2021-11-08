namespace TokanPages.Backend.Shared.Services
{
    using Models;

    public class ApplicationSettings : IApplicationSettings
    {
        public ApplicationPaths ApplicationPaths { get; }

        public IdentityServer IdentityServer { get; }

        public ExpirationSettings ExpirationSettings { get; }

        public EmailSender EmailSender { get; }

        public AzureStorage AzureStorage { get; }

        public SonarQube SonarQube { get; }

        public ApplicationSettings(AzureStorage azureStorage, ApplicationPaths applicationPaths, IdentityServer identityServer, 
            ExpirationSettings expirationSettings, EmailSender emailSender, SonarQube sonarQube)
        {
            AzureStorage = azureStorage;
            ApplicationPaths = applicationPaths;
            IdentityServer = identityServer;
            ExpirationSettings = expirationSettings;
            EmailSender = emailSender;
            SonarQube = sonarQube;
        }
    }
}