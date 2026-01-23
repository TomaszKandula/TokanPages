using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain;

[ExcludeFromCodeCoverage]
public abstract class Entity<TKey>
{
    [Key]
    [PrimaryKey]
    public TKey Id { get; init; }
}