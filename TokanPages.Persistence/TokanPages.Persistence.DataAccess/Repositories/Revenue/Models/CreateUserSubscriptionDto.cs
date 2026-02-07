using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class CreateUserSubscriptionDto : UserSubscriptionBaseDto
{
    public Guid? Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CreatedBy { get; set; }
}