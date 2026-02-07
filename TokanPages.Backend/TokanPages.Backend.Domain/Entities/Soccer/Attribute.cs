using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Attributes")]
public class Attribute : Entity<Guid>
{
    public string Name { get; set; }

    public double Coefficient { get; set; }
}