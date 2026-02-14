using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserSubscriptionDto : UserSubscriptionBaseDto
{
    public required bool AutoRenewal { get; init; }

    public required Guid ModifiedBy { get; init; }

    public required bool IsActive { get; init; }

    public DateTime? CompletedAt { get; init; }

    public DateTime? ExpiresAt { get; init; }
}