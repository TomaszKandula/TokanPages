using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPaymentHistoryDto
{
    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyIso { get; set; } = string.Empty;

    public TermType Term { get; set; }

    public Guid CreatedBy { get; set; }
}