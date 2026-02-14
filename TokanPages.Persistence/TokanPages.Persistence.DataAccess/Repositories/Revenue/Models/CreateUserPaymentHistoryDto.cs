using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPaymentHistoryDto
{
    public required Guid UserId { get; init; }

    public required decimal Amount { get; init; }

    public required string CurrencyIso { get; init; }

    public required TermType Term { get; init; }

    public required Guid CreatedBy { get; init; }
}