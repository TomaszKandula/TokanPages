using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Subscribers;

/// <summary>
/// Use it when you want to remove existing subscriber.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveSubscriberDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }
}