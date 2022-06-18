namespace TokanPages.Backend.Dto.Subscribers;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to add newsletter subscriber
/// </summary>
[ExcludeFromCodeCoverage]
public class AddSubscriberDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Email { get; set; }
}