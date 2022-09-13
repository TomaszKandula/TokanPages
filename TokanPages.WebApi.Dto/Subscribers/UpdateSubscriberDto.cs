using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Subscribers;

/// <summary>
/// Use it when you want to update existing subscriber
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateSubscriberDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Email { get; set; }
        
    /// <summary>
    /// Optional
    /// </summary>
    public bool? IsActivated { get; set; }
        
    /// <summary>
    /// Optional
    /// </summary>
    public int? Count { get; set; }
}