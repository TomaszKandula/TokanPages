namespace TokanPages.Backend.Shared.Services
{
    using Models;

    public interface IApplicationSettings
    {
        ApplicationPaths ApplicationPaths { get; }

        IdentityServer IdentityServer { get; }

        ExpirationSettings ExpirationSettings { get; }

        EmailSender EmailSender { get; }

        AzureStorage AzureStorage { get; }

        SonarQube SonarQube { get; }
    }
}