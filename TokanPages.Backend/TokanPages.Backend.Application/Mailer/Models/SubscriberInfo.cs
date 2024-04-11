using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Mailer.Models;

/// <summary>
/// Subscriber basic data.
/// </summary>
[ExcludeFromCodeCoverage]
public class SubscriberInfo
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Identification.
    /// </summary>
    public string Id { get; set; } = "";
}