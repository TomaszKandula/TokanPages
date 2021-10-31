namespace TokanPages.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class UserRefreshTokens : Entity<Guid>
    {
        public Guid UserId { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Token { get; set; }
        
        [Required]
        public DateTime Expires { get; set; }
        
        [Required]
        public DateTime Created { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string CreatedByIp { get; set; }
        
        public DateTime? Revoked { get; set; }
        
        [MaxLength(15)]
        public string RevokedByIp { get; set; }
        
        [MaxLength(500)]
        public string ReplacedByToken { get; set; }
        
        [MaxLength(255)]
        public string ReasonRevoked { get; set; }
        
        public Users User { get; set; }
    }
}