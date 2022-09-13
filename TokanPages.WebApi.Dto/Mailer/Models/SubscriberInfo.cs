using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Mailer.Models;

/// <summary>
/// Subscriber basic data
/// </summary>
[ExcludeFromCodeCoverage]
public class SubscriberInfo
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Mandatory
    /// </summary>
    public string Id { get; set; } = "";
}