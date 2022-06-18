namespace TokanPages.Backend.Shared.Services.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ApplicationPaths
{
    public string UpdateSubscriberPath { get; set; } = "";

    public string UnsubscribePath { get; set; } = "";

    public string UpdatePasswordPath { get; set; } = "";

    public string ActivationPath { get; set; } = "";

    public string DevelopmentOrigin { get; set; } = "";

    public string DeploymentOrigin { get; set; } = "";
}