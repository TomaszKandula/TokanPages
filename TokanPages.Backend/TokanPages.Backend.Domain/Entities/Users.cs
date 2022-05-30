namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class Users : Entity<Guid>, IAuditable, ISoftDelete
{
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

    public bool IsActivated { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<Articles> ArticlesNavigation { get; set; } = new HashSet<Articles>();

    public ICollection<ArticleLikes> ArticleLikesNavigation { get; set; } = new HashSet<ArticleLikes>();

    public ICollection<ArticleCounts> ArticleCountsNavigation { get; set; } = new HashSet<ArticleCounts>();

    public ICollection<Albums> AlbumsNavigation { get; set; } = new HashSet<Albums>();

    public ICollection<UserPhotos> UserPhotosNavigation { get; set; } = new HashSet<UserPhotos>();

    public ICollection<UserPermissions> UserPermissionsNavigation { get; set; } = new HashSet<UserPermissions>();

    public ICollection<UserRoles> UserRolesNavigation { get; set; } = new HashSet<UserRoles>();

    public ICollection<UserTokens> UserTokensNavigation { get; set; } = new HashSet<UserTokens>();

    public ICollection<UserRefreshTokens> UserRefreshTokensNavigation { get; set; } = new HashSet<UserRefreshTokens>();

    public ICollection<UserInfo> UserInfoNavigation { get; set; } = new HashSet<UserInfo>();
}