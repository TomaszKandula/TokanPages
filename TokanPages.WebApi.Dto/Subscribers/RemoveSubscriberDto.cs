namespace TokanPages.Backend.Dto.Subscribers;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to remove existing subscriber
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveSubscriberDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }
}