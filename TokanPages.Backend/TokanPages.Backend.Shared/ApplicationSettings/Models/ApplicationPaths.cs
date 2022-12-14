using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.ApplicationSettings.Models;

[ExcludeFromCodeCoverage]
public class ApplicationPaths
{
    public string UpdateSubscriberPath { get; set; } = "";

    public string UnsubscribePath { get; set; } = "";

    public string UpdatePasswordPath { get; set; } = "";

    public string ActivationPath { get; set; } = "";

    public string DevelopmentOrigin { get; set; } = "";

    public string DeploymentOrigin { get; set; } = "";

    public Templates Templates { get; set; } = new();
}