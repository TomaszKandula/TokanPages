using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public class Photos : Entity<Guid>
    {
        public Guid UserId { get; set; }

        public Users User { get; set; }
        
        public Guid PhotoGearId { get; set; }
        
        public PhotoGears PhotoGear { get; set; }

        public Guid PhotoCategoryId { get; set; }
        
        public PhotoCategories PhotoCategory { get; set; }
        
        [MaxLength(500)]
        public string Keywords { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string PhotoUrl { get; set; }
        
        [Required]
        public DateTime DateTaken { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public ICollection<Albums> Albums { get; set; } = new HashSet<Albums>();
    }
}
