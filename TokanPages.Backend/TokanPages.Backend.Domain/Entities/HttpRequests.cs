using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities;

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