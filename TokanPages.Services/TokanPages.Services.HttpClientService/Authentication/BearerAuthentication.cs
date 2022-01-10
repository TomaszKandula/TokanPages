namespace TokanPages.Services.HttpClientService.Authentication;

using System.Diagnostics.CodeAnalysis;
using Abstractions;

[ExcludeFromCodeCoverage]
public class BearerAuthentication : IAuthentication
{
    public string Token { get; set; }
}