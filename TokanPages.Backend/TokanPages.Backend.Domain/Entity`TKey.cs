namespace TokanPages.Backend.Domain
{
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public abstract class Entity<TKey>
    {
        [Key]
        public TKey Id { get; init; }
    }
}