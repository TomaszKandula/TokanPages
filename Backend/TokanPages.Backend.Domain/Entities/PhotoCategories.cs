using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public class PhotoCategories : Entity<Guid>
    {
        public PhotoCategories() => Photos = new HashSet<Photos>();

        [Required]
        [MaxLength(60)]
        public string CategoryName { get; set; }
        
        public ICollection<Photos> Photos { get; set; }
    }
}
