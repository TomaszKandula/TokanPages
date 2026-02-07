using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "VatNumberPatterns")]
public class VatNumberPattern : Entity<Guid>
{
    public string CountryCode { get; set; }

    public string Pattern { get; set; }
}