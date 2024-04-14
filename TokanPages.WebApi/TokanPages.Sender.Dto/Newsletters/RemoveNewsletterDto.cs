using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Sender.Dto.Newsletters;

/// <summary>
/// Use it when you want to remove existing subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveNewsletterDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }
}