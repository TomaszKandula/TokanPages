using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.Services.PayUService.Models;

[ExcludeFromCodeCoverage]
public class AuthorizationOutput
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    [JsonProperty("expires_in")]
    public string? ExpiresIn { get; set; }

    [JsonProperty("grant_type")]
    public string? GrantType { get; set; }
}