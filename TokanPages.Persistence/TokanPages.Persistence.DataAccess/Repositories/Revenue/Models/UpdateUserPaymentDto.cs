using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserPaymentDto : UserPaymentBaseDto
{
    public required Guid ModifiedBy { get; init; }

    public required Guid CreatedBy { get; init; }

    public required DateTime CreatedAt { get; init; }
}