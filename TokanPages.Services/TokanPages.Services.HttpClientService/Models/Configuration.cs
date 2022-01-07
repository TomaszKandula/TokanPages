#nullable enable
namespace TokanPages.Services.HttpClientService.Models;

using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Authentication;

[ExcludeFromCodeCoverage]
public class Configuration
{
    public string Url { get; set; } = "";

    public string Method { get; set; } = "";

    public IDictionary<string, string>? Headers { get; set; }
    
    public IAuthentication? Authentication { get; set; }

    public StringContent? StringContent { get; set; } 
}