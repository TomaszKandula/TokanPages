using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Tests.UnitTests.Models;

[DatabaseTable(Schema = "soccer")]
internal class TestPlayerTwo
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public bool IsPublished { get; set; }
}
