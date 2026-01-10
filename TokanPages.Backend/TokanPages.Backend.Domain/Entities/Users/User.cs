using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
public class User : Entity<Guid>, IAuditable, ISoftDelete
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
    public ICollection<UserCompany> UserCompanies { get; set; } = new HashSet<UserCompany>();
    public ICollection<UserBankAccount> UserBankAccounts { get; set; } = new HashSet<UserBankAccount>();
    public ICollection<IssuedInvoice> IssuedInvoices { get; set; } = new HashSet<IssuedInvoice>();
    public ICollection<BatchInvoice> BatchInvoices { get; set; } = new HashSet<BatchInvoice>();
    public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    public ICollection<ArticleLike> ArticleLikes { get; set; } = new HashSet<ArticleLike>();
    public ICollection<ArticleCount> ArticleCounts { get; set; } = new HashSet<ArticleCount>();
    public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    public ICollection<UserPhoto> UserPhotos { get; set; } = new HashSet<UserPhoto>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public ICollection<UserToken> UserTokens { get; set; } = new HashSet<UserToken>();
    public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
    public ICollection<UserInfo> UserInformation { get; set; } = new HashSet<UserInfo>();
    public ICollection<UserPayment> UserPayments { get; set; } = new HashSet<UserPayment>();
    public ICollection<UserPaymentHistory> UserPaymentsHistory { get; set; } = new HashSet<UserPaymentHistory>();
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = new HashSet<UserSubscription>();
    public ICollection<UserNote> UserNotes { get; set; } = new HashSet<UserNote>();
}