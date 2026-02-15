using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UserPaymentBaseDto
{
    public required string ExtOrderId { get; init; }

    public required string PmtOrderId { get; init; }

    public required string PmtStatus { get; init; }

    public required string PmtType { get; init; }

    public required string PmtToken { get; init; }
}