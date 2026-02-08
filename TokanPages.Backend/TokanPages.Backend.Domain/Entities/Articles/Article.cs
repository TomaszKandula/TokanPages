using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Articles")]
public class Article : Entity<Guid>, IAuditable
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required bool IsPublished { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CategoryId {get; set; }

    public required string LanguageIso { get; set; }
}