using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UserPaymentBaseDto
{
    public string ExtOrderId { get; set; } = string.Empty;

    public string PmtOrderId { get; set; } = string.Empty;

    public string PmtStatus { get; set; } = string.Empty;

    public string PmtType { get; set; } = string.Empty;

    public string PmtToken { get; set; } = string.Empty;
}