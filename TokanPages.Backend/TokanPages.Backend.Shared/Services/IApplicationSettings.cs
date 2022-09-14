using TokanPages.Backend.Shared.Services.Models;

namespace TokanPages.Backend.Shared.Services;

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