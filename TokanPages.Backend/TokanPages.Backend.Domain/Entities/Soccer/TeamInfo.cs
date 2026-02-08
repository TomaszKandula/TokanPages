using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "TeamInfo")]
public class TeamInfo : Entity<Guid>
{
    public required Guid TeamId { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Avatar { get; set; }

    public required string ImageBlobName { get; set; }
}