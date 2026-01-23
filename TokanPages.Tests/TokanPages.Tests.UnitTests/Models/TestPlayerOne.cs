using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Tests.UnitTests.Models;

[DatabaseTable(Schema = "soccer", TableName = "Players")]
internal class TestPlayerOne
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Likes { get; set; }
}
