using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class BasicAuthentication : IAuthentication
{
    public string Login { get; set; } = "";

    public string Password { get; set; } = "";
}