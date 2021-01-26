using System;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Users : Entity<Guid>
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
    }
}
