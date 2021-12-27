namespace TokanPages.Backend.Shared.Services;

using Models;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ApplicationSettings : IApplicationSettings
{
    public ApplicationPaths ApplicationPaths { get; }

    public IdentityServer IdentityServer { get; }

    public ExpirationSettings ExpirationSettings { get; }

    public EmailSender EmailSender { get; }

    public AzureStorage AzureStorage { get; }

    public AzureRedis AzureRedis { get; }

    public SonarQube SonarQube { get; }

    public ApplicationSettings(AzureStorage azureStorage, AzureRedis azureRedis, ApplicationPaths applicationPaths, IdentityServer identityServer, 
        ExpirationSettings expirationSettings, EmailSender emailSender, SonarQube sonarQube)
    {
        AzureStorage = azureStorage;
        AzureRedis = azureRedis;
        ApplicationPaths = applicationPaths;
        IdentityServer = identityServer;
        ExpirationSettings = expirationSettings;
        EmailSender = emailSender;
        SonarQube = sonarQube;
    }
}