using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "InvoiceTemplates")]
public class InvoiceTemplate : Entity<Guid>, ISoftDelete
{
    public required string Name { get; set; }

    public required byte[] Data { get; set; }

    public required string ContentType { get; set; }

    public required string ShortDescription { get; set; }

    public required DateTime GeneratedAt { get; set; }

    public required bool IsDeleted { get; set; }
}