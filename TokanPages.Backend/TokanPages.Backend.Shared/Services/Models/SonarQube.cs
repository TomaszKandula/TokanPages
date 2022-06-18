namespace TokanPages.Backend.Shared.Services.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SonarQube
{
    public string Server { get; set; } = "";

    public string Token { get; set; } = "";
}