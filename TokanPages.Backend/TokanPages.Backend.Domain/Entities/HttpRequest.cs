using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "HttpRequests")]
public class HttpRequest : Entity<Guid>
{
    [Required]
    [MaxLength(15)]
    public string SourceAddress { get; set; }
    public DateTime RequestedAt { get; set; }
    [Required]
    [MaxLength(150)]
    public string RequestedHandlerName { get; set; }    
}