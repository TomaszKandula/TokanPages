using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Articles : Entity<Guid>
    {
        public Articles() 
        {
            Likes = new HashSet<Likes>();
        }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public int ReadCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid UserId { get; set; }

        public Users User { get; set; }

        public ICollection<Likes> Likes { get; set; }
    }
}
