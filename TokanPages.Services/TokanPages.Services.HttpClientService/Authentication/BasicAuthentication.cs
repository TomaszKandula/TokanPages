namespace TokanPages.Services.HttpClientService.Authentication;

using System.Diagnostics.CodeAnalysis;
using Abstractions;

[ExcludeFromCodeCoverage]
public class BasicAuthentication : IAuthentication
{
    public string Login { get; set; }

    public string Password { get; set; }
}