using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPaymentDto : UserPaymentBaseDto
{
    public Guid? Id { get; init; }

    public required Guid UserId { get; init; }

    public required Guid CreatedBy { get; init; }
}