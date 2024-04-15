using TokanPages.Backend.Domain.Enums;
using MediatR;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class AddSubscriptionCommand : IRequest<AddSubscriptionCommandResult>
{
    public Guid? UserId { get; set; }

    public TermType SelectedTerm { get; set; }

    public string? UserCurrency { get; set; }

    public string? UserLanguage { get; set; }
}