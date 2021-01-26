using System;
using System.ComponentModel.DataAnnotations;

namespace TokanPages.Backend.Core.Entities
{
    public abstract class Entity<TKey>
    {
        [Key]
        public Guid Id { get; set; }
    }
}
