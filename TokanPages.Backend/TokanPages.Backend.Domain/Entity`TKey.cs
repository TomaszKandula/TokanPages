using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain;

[ExcludeFromCodeCoverage]
public abstract class Entity<TKey>
{
    [Key]
    public TKey Id { get; init; }
}