namespace TokanPages.Backend.Shared.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class IdentityServer
{
    public string Issuer { get; set; } = "";
        
    public string Audience { get; set; } = "";
        
    public string WebSecret { get; set; } = "";
        
    public bool RequireHttps { get; set; }
        
    public int WebTokenExpiresIn { get; set; }
        
    public int RefreshTokenExpiresIn { get; set; }
}