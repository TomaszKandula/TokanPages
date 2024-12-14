using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class OrderSpaCachingCommandValidator : AbstractValidator<OrderSpaCachingCommand>
{
    public OrderSpaCachingCommandValidator()
    {
        RuleFor(command => command.GetUrl)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
        
        RuleFor(command => command.PostUrl)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}