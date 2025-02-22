﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Backend.Domain.Entities.User;

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
    public bool IsVerified { get; set; }
    public bool IsDeleted { get; set; }
    public bool HasBusinessLock { get; set; }

    /* Navigation properties */
    public ICollection<UserCompanies> UserCompanies { get; set; } = new HashSet<UserCompanies>();
    public ICollection<UserBankAccounts> UserBankAccounts { get; set; } = new HashSet<UserBankAccounts>();
    public ICollection<IssuedInvoices> IssuedInvoices { get; set; } = new HashSet<IssuedInvoices>();
    public ICollection<BatchInvoices> BatchInvoices { get; set; } = new HashSet<BatchInvoices>();
    public ICollection<Articles> Articles { get; set; } = new HashSet<Articles>();
    public ICollection<ArticleLikes> ArticleLikes { get; set; } = new HashSet<ArticleLikes>();
    public ICollection<ArticleCounts> ArticleCounts { get; set; } = new HashSet<ArticleCounts>();
    public ICollection<Albums> Albums { get; set; } = new HashSet<Albums>();
    public ICollection<UserPhotos> UserPhotos { get; set; } = new HashSet<UserPhotos>();
    public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
    public ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();
    public ICollection<UserTokens> UserTokens { get; set; } = new HashSet<UserTokens>();
    public ICollection<UserRefreshTokens> UserRefreshTokens { get; set; } = new HashSet<UserRefreshTokens>();
    public ICollection<UserInfo> UserInfo { get; set; } = new HashSet<UserInfo>();
    public ICollection<UserPayment> UserPayment { get; set; } = new HashSet<UserPayment>();
    public ICollection<UserPaymentHistory> UserPaymentHistory { get; set; } = new HashSet<UserPaymentHistory>();
    public ICollection<UserSubscription> UserSubscription { get; set; } = new HashSet<UserSubscription>();
    public ICollection<UserNote> UserNote { get; set; } = new HashSet<UserNote>();
}