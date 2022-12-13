using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Subscribers;

/// <summary>
/// Use it when you want to add newsletter subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddSubscriberDto
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string? Email { get; set; }
}