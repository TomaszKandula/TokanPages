using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    /* Category: Article */
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
    public virtual DbSet<ArticleLike> ArticleLikes { get; set; }
    public virtual DbSet<ArticleCount> ArticleCounts { get; set; }
    public virtual DbSet<ArticleTag> ArticleTags { get; set; }

    /* Category: Invoicing */
    public virtual DbSet<BatchInvoiceItem> BatchInvoiceItems { get; set; }
    public virtual DbSet<BatchInvoice> BatchInvoices { get; set; }
    public virtual DbSet<BatchInvoiceProcessing> BatchInvoicesProcessing { get; set; }
    public virtual DbSet<InvoiceTemplate> InvoiceTemplates { get; set; }
    public virtual DbSet<IssuedInvoice> IssuedInvoices { get; set; }
    public virtual DbSet<UserBankAccount> UserBankAccounts { get; set; }
    public virtual DbSet<UserCompany> UserCompanies { get; set; }
    public virtual DbSet<VatNumberPattern> VatNumberPatterns { get; set; }

    /* Category: Notification */
    public virtual DbSet<PushNotification> PushNotifications { get; set; }
    public virtual DbSet<PushNotificationTag> PushNotificationTags { get; set; }
    public virtual DbSet<PushNotificationLog> PushNotificationLogs { get; set; }
    public virtual DbSet<WebNotification> WebNotifications { get; set; }

    /* Category: Photography */
    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<UserPhoto> UserPhotos { get; set; }
    public virtual DbSet<PhotoGear> PhotoGears { get; set; }
    public virtual DbSet<PhotoCategory> PhotoCategories { get; set; }

    /* Category: User */
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<DefaultPermission> DefaultPermissions { get; set; }
    public virtual DbSet<UserPermission> UserPermissions { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }
    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public virtual DbSet<UserInfo> UserInformation { get; set; }
    public virtual DbSet<UserMessage> UserMessages { get; set; }
    public virtual DbSet<UserMessageCache> UserMessagesCache { get; set; }
    public virtual DbSet<UserPayment> UserPayments { get; set; }
    public virtual DbSet<UserPaymentHistory> UserPaymentsHistory { get; set; }
    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
    public virtual DbSet<UserNote> UserNotes { get; set; }

    /* Category: Other */
    public virtual DbSet<Language> Languages { get; set; }
    public virtual DbSet<CategoryName> CategoryNames { get; set; }
    public virtual DbSet<Newsletter> Newsletters { get; set; }
    public virtual DbSet<HttpRequest> HttpRequests { get; set; }
    public virtual DbSet<UploadedVideo> UploadedVideos { get; set; }
    public virtual DbSet<ServiceBusMessage> ServiceBusMessages { get; set; }
    public virtual DbSet<SubscriptionPricing> SubscriptionsPricing { get; set; }
    public virtual DbSet<BusinessInquiry> BusinessInquiries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}