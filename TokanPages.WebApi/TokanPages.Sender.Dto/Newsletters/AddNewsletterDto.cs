using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Sender.Dto.Newsletters;

/// <summary>
/// Use it when you want to add newsletter subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddNewsletterDto
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string? Email { get; set; }
}