namespace TokanPages.Backend.Shared.Services.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ExpirationSettings
{
    public int ResetIdExpiresIn { get; set; }

    public int ActivationIdExpiresIn { get; set; }

    public Likes Likes { get; set; } = new();
}