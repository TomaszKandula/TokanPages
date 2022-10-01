using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Shared.Services.Models;

namespace TokanPages.Backend.Shared.Services;

[ExcludeFromCodeCoverage]
public class ApplicationSettings : IApplicationSettings
{
    public ApplicationPaths ApplicationPaths { get; }

    public IdentityServer IdentityServer { get; }

    public LimitSettings LimitSettings { get; }

    public EmailSender EmailSender { get; }

    public AzureStorage AzureStorage { get; }

    public AzureRedis AzureRedis { get; }

    public SonarQube SonarQube { get; }

    public ApplicationSettings(AzureStorage azureStorage, AzureRedis azureRedis, ApplicationPaths applicationPaths, 
        IdentityServer identityServer, LimitSettings limitSettings, EmailSender emailSender, SonarQube sonarQube)
    {
        AzureStorage = azureStorage;
        AzureRedis = azureRedis;
        ApplicationPaths = applicationPaths;
        IdentityServer = identityServer;
        LimitSettings = limitSettings;
        EmailSender = emailSender;
        SonarQube = sonarQube;
    }
}