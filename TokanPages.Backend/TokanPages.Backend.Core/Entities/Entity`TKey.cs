using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace TokanPages.Backend.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity<TKey>
    {
        [Key]
        public TKey Id { get; init; }
    }
}
