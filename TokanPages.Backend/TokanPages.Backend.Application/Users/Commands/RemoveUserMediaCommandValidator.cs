namespace TokanPages.Backend.Application.Users.Commands;

using FluentValidation;
using Shared.Resources;

public class RemoveUserMediaCommandValidator : AbstractValidator<RemoveUserMediaCommand>
{
    public RemoveUserMediaCommandValidator()
    {
        RuleFor(command => command.UniqueBlobName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(2048)
            .WithErrorCode(nameof(ValidationCodes.NAME_TOO_LONG))
            .WithMessage(ValidationCodes.NAME_TOO_LONG);
    }
}
