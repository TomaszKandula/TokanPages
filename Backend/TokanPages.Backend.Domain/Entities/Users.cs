using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Users : Entity<Guid>
    {
        public Users() 
        {
            Articles = new HashSet<Articles>();
            ArticleLikes = new HashSet<ArticleLikes>();
            Photos = new HashSet<Photos>();
            Albums = new HashSet<Albums>();
        }

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

        public ICollection<Articles> Articles { get; set; }

        public ICollection<ArticleLikes> ArticleLikes { get; set; }
        
        public ICollection<Photos> Photos { get; set; }
        
        public ICollection<Albums> Albums { get; set; }
    }
}
