using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class OrderDetail : OrderDetailBase<InvoiceItem>
{
    public Guid UserId { get; set; }

    public Guid UserCompanyId { get; set; }

    public Guid UserBankAccountId { get; set; }

    public string InvoiceNumber { get; set; } = "";

    public DateTime DueDate { get; set; }
}