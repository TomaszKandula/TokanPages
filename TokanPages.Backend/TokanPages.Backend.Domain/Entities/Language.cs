using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Languages")]
public class Language : Entity<Guid>, IHasSortOrder
{
    public required string LangId { get; set; }

    public required string HrefLang { get; set; }

    public required string Name { get; set; }

    public required bool IsDefault { get; set; }

    public required int SortOrder { get; set; }
}