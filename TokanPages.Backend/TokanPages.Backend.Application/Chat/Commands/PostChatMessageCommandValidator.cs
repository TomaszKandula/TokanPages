using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Chat.Commands;

public class PostChatMessageCommandValidator : AbstractValidator<PostChatMessageCommand>
{
    public PostChatMessageCommandValidator()
    {
        RuleFor(command => command.Message)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}