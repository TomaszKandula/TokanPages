using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Fields")]
public class Field : Entity<Guid>, ISoftDelete
{
    public string Name { get; set; }

    public string Description { get; set; }    

    public double GpsLatitude { get; set; }

    public double GpsLongitude { get; set; }

    public DateTime Published { get; set; }

    public bool IsDeleted { get; set; }
}