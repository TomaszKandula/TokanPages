using System.Diagnostics.CodeAnalysis;
using TokanPages.Revenue.Dto.Payments.Sections;

namespace TokanPages.Revenue.Dto.Payments;

/// <summary>
/// Notification object send by payment provider (PayU).
/// </summary>
[ExcludeFromCodeCoverage]
public class PostNotificationDto
{
    /// <summary>
    /// Order.
    /// </summary>
    public OrderDto? Order { get; set; }

    /// <summary>
    /// Receipt date and time.
    /// </summary>
    public DateTime? LocalReceiptDateTime { get; set; }

    /// <summary>
    /// Properties.
    /// </summary>
    public List<PropertyDto>? Properties { get; set; } = new();
}