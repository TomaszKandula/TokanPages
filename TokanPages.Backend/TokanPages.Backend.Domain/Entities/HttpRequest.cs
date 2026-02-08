using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "HttpRequests")]
public class HttpRequest : Entity<Guid>
{
    public required string SourceAddress { get; set; }

    public required DateTime RequestedAt { get; set; }

    public required string RequestedHandlerName { get; set; }    
}