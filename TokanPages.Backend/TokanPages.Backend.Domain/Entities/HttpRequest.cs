using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "HttpRequests")]
public class HttpRequest : Entity<Guid>
{
    public string SourceAddress { get; set; }

    public DateTime RequestedAt { get; set; }

    public string RequestedHandlerName { get; set; }    
}