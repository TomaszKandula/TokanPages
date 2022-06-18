namespace TokanPages.Backend.Shared.Services.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class LimitSettings
{
    public int ResetIdExpiresIn { get; set; }

    public int ActivationIdExpiresIn { get; set; }

    public Likes Likes { get; set; } = new();
}