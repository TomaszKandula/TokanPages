namespace TokanPages.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;
    using TokanPages.Backend.Core.Entities;

    [ExcludeFromCodeCoverage]
    public class UserTokens : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        [MaxLength(15)]
        public string CreatedByIp { get; set; }

        [Required]
        [MaxLength(255)]
        public string Command { get; set; }

        public Users User { get; set; }
    }
}