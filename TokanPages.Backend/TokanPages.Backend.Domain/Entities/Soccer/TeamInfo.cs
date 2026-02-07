using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "TeamInfo")]
public class TeamInfo : Entity<Guid>
{
    public Guid TeamId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Avatar { get; set; }

    public string ImageBlobName { get; set; }
}