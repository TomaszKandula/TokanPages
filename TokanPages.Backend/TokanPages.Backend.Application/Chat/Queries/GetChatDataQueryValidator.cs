using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Chat.Queries;

public class GetChatDataQueryValidator : AbstractValidator<GetChatDataQuery>
{
    public GetChatDataQueryValidator()
    {
        RuleFor(query => query.ChatKey)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}