namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class Users : Entity<Guid>, IAuditable
{
    public bool IsActivated { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserAlias { get; set; }

    [Required]
    [MaxLength(255)]
    public string EmailAddress { get; set; }

    [Required]
    [MaxLength(100)]
    public string CryptedPassword { get; set; }

    public Guid? ResetId { get; set; }

    public DateTime? ResetIdEnds { get; set; }

    public Guid? ActivationId { get; set; }

    public DateTime? ActivationIdEnds { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public ICollection<Articles> Articles { get; set; } = new HashSet<Articles>();

    public ICollection<ArticleLikes> ArticleLikes { get; set; } = new HashSet<ArticleLikes>();

    public ICollection<ArticleCounts> ArticleCounts { get; set; } = new HashSet<ArticleCounts>();

    public ICollection<Photos> Photos { get; set; } = new HashSet<Photos>();
        
    public ICollection<Albums> Albums { get; set; } = new HashSet<Albums>();
        
    public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();

    public ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();

    public ICollection<UserTokens> UserTokens { get; set; } = new HashSet<UserTokens>();

    public ICollection<UserRefreshTokens> UserRefreshTokens { get; set; } = new HashSet<UserRefreshTokens>();

    public ICollection<UserInfo> UserInfoNavigation { get; set; } = new HashSet<UserInfo>();
}