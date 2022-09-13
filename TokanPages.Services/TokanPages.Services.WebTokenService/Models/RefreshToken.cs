using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.WebTokenService.Models;

[ExcludeFromCodeCoverage]
public class RefreshToken
{
    public string Token { get; set; } = "";

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public string? CreatedByIp { get; set; }
}