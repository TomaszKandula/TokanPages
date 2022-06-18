namespace TokanPages.Backend.Shared.Services;

using Models;

public interface IApplicationSettings
{
    ApplicationPaths ApplicationPaths { get; }

    IdentityServer IdentityServer { get; }

    LimitSettings LimitSettings { get; }

    EmailSender EmailSender { get; }

    AzureStorage AzureStorage { get; }

    AzureRedis AzureRedis { get; }

    SonarQube SonarQube { get; }
}