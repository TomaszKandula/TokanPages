using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Sender.Dto.Newsletters;

/// <summary>
/// Use it when you want to update existing subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateNewsletterDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email address.
    /// </summary>
    public string? Email { get; set; }
        
    /// <summary>
    /// Activation flag.
    /// </summary>
    public bool? IsActivated { get; set; }
        
    /// <summary>
    /// Count number.
    /// </summary>
    public int? Count { get; set; }
}