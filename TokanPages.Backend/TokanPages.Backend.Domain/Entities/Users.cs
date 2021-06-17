using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Users : Entity<Guid>
    {
        [Required]
        [MaxLength(255)]
        public string UserAlias { get; set; }

        public bool IsActivated { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string EmailAddress { get; set; }

        public DateTime Registered { get; set; }

        public DateTime? LastLogged { get; set; }

        public DateTime? LastUpdated { get; set; }

        [MaxLength(255)]
        public string AvatarName { get; set; }

        [MaxLength(255)]
        public string ShortBio { get; set; }

        public ICollection<Articles> Articles { get; set; } = new HashSet<Articles>();

        public ICollection<ArticleLikes> ArticleLikes { get; set; } = new HashSet<ArticleLikes>();
        
        public ICollection<Photos> Photos { get; set; } = new HashSet<Photos>();
        
        public ICollection<Albums> Albums { get; set; } = new HashSet<Albums>();
    }
}
