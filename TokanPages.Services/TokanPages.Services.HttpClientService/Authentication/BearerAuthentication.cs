namespace TokanPages.Services.HttpClientService.Authentication;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class BearerAuthentication : IAuthentication
{
    public string Token { get; set; }
}