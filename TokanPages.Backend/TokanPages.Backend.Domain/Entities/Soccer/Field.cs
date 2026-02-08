using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Fields")]
public class Field : Entity<Guid>, ISoftDelete
{
    public required string Name { get; set; }

    public required string Description { get; set; }    

    public required double GpsLatitude { get; set; }

    public required double GpsLongitude { get; set; }

    public required DateTime Published { get; set; }

    public required bool IsDeleted { get; set; }
}