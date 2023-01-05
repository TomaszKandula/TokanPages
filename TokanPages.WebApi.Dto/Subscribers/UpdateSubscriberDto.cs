using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Subscribers;

/// <summary>
/// Use it when you want to update existing subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateSubscriberDto
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