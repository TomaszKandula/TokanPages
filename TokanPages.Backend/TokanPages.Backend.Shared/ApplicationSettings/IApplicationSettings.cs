using TokanPages.Backend.Shared.ApplicationSettings.Models;

namespace TokanPages.Backend.Shared.ApplicationSettings;

public interface IApplicationSettings
{
    ApplicationPaths ApplicationPaths { get; }

    IdentityServer IdentityServer { get; }

    LimitSettings LimitSettings { get; }

    EmailSender EmailSender { get; }

    AzureStorage AzureStorage { get; }

    AzureRedis AzureRedis { get; }
}