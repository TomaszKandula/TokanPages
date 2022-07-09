namespace TokanPages.Backend.Shared.Services.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Likes
{
    public int ForAnonymous { get; set; }

    public int ForUser { get; set; }
}