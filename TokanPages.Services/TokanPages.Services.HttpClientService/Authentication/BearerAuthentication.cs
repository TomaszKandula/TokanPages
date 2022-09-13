using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Authentication;

[ExcludeFromCodeCoverage]
public class BearerAuthentication : IAuthentication
{
    public string Token { get; set; } = "";
}