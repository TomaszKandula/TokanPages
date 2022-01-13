namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class HttpRequests : Entity<Guid>
{
    [Required]
    [MaxLength(15)]
    public string SourceAddress { get; set; }

    public DateTime RequestedAt { get; set; }

    [Required]
    [MaxLength(150)]
    public string RequestedHandlerName { get; set; }    
}