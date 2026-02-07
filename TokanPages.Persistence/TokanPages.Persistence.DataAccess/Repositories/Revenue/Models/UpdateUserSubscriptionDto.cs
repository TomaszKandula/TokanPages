using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserSubscriptionDto : UserSubscriptionBaseDto
{
    public bool AutoRenewal { get; set; }

    public Guid ModifiedBy { get; set; }
}