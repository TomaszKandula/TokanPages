using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Articles")]
public class Article : Entity<Guid>, IAuditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPublished { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UserId { get; set; }
    public Guid? CategoryId {get; set; }
    public string LanguageIso { get; set; }
}