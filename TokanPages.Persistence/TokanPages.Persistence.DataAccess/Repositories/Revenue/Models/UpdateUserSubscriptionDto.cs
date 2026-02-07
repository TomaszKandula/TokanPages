using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserSubscriptionDto : UserSubscriptionBaseDto
{
    public bool AutoRenewal { get; set; }

    public Guid ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}