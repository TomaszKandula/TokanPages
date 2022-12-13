using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.ApplicationSettings.Models;

[ExcludeFromCodeCoverage]
public class Likes
{
    public int ForAnonymous { get; set; }

    public int ForUser { get; set; }
}