using TokanPages.Backend.Domain.Enums;
using MediatR;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class UpdateSubscriptionCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public string? ExtOrderId { get; set; }

    public bool? AutoRenewal { get; set; }

    public TermType? Term { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? CurrencyIso { get; set; }
}