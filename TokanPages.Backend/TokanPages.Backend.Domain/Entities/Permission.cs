using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Permissions")]
public class Permission : Entity<Guid>, IAuditable
{
    public string Name { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}