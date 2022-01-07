namespace TokanPages.Services.HttpClientService.Authentication;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class BasicAuthentication : IAuthentication
{
    public string Login { get; set; }

    public string Password { get; set; }
}