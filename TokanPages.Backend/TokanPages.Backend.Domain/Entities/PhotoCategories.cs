using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class PhotoCategories : Entity<Guid>
    {
        [Required]
        [MaxLength(60)]
        public string CategoryName { get; set; }
        
        public ICollection<Photos> Photos { get; set; } = new HashSet<Photos>();
    }
}
