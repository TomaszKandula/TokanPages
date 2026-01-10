using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Notification;

namespace TokanPages.Persistence.Database;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    /* Category: Article */
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
    public virtual DbSet<ArticleLike> ArticleLikes { get; set; }
    public virtual DbSet<ArticleCount> ArticleCounts { get; set; }
    public virtual DbSet<ArticleTags> ArticleTags { get; set; }

    /* Category: Invoicing */
    public virtual DbSet<BatchInvoiceItems> BatchInvoiceItems { get; set; }
    public virtual DbSet<BatchInvoices> BatchInvoices { get; set; }
    public virtual DbSet<BatchInvoicesProcessing> BatchInvoicesProcessing { get; set; }
    public virtual DbSet<InvoiceTemplates> InvoiceTemplates { get; set; }
    public virtual DbSet<IssuedInvoices> IssuedInvoices { get; set; }
    public virtual DbSet<UserBankAccounts> UserBankAccounts { get; set; }
    public virtual DbSet<UserCompanies> UserCompanies { get; set; }
    public virtual DbSet<VatNumberPatterns> VatNumberPatterns { get; set; }

    /* Category: Notification */
    public virtual DbSet<PushNotification> PushNotifications { get; set; }
    public virtual DbSet<PushNotificationTag> PushNotificationTags { get; set; }
    public virtual DbSet<PushNotificationLog> PushNotificationLogs { get; set; }
    public virtual DbSet<WebNotification> WebNotifications { get; set; }

    /* Category: Photography */
    public virtual DbSet<Albums> Albums { get; set; }
    public virtual DbSet<UserPhotos> UserPhotos { get; set; }
    public virtual DbSet<PhotoGears> PhotoGears { get; set; }
    public virtual DbSet<PhotoCategories> PhotoCategories { get; set; }

    /* Category: User */
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
    public virtual DbSet<Permissions> Permissions { get; set; }
    public virtual DbSet<DefaultPermissions> DefaultPermissions { get; set; }
    public virtual DbSet<UserPermissions> UserPermissions { get; set; }
    public virtual DbSet<UserRoles> UserRoles { get; set; }
    public virtual DbSet<UserTokens> UserTokens { get; set; }
    public virtual DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }
    public virtual DbSet<UserInfo> UserInfo { get; set; }
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
    public virtual DbSet<SubscriptionPricing> SubscriptionPricing { get; set; }
    public virtual DbSet<BusinessInquiry> BusinessInquiry { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}