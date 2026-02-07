using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Languages")]
public class Language : Entity<Guid>, IHasSortOrder
{
    public string LangId { get; set; }

    public string HrefLang { get; set; }

    public string Name { get; set; }

    public bool IsDefault { get; set; }

    public int SortOrder { get; set; }
}