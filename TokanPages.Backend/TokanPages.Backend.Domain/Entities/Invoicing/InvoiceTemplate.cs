using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "InvoiceTemplates")]
public class InvoiceTemplate : Entity<Guid>, ISoftDelete
{
    public string Name { get; set; }

    public byte[] Data { get; set; }

    public string ContentType { get; set; }

    public string ShortDescription { get; set; }

    public DateTime GeneratedAt { get; set; }

    public bool IsDeleted { get; set; }
}