using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserSubscriptionDto : UserSubscriptionBaseDto
{
    public Guid ModifiedBy { get; set; }
}