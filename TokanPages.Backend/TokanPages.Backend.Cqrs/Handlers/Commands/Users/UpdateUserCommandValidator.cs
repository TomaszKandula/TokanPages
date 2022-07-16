namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using FluentValidation;
using Shared.Resources;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator() 
    {
        When(command => command.Id != null, () =>
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.UserAlias != null, () =>
        {
            RuleFor(command => command.UserAlias)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.USERALIAS_TOO_LONG))
                .WithMessage(ValidationCodes.USERALIAS_TOO_LONG);
        });

        When(command => command.FirstName != null, () =>
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.FIRST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.FIRST_NAME_TOO_LONG);
        });

        When(command => command.LastName != null, () =>
        {
            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.LAST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.LAST_NAME_TOO_LONG);
        });

        When(command => command.EmailAddress != null, () =>
        {
            RuleFor(command => command.EmailAddress)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
        });

        When(command => command.ShortBio != null, () =>
        {
            RuleFor(command => command.ShortBio)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);
        });

        When(command => command.UserImageName != null, () =>
        {
            RuleFor(command => command.UserImageName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.TOO_LONG_USER_IMAGE_NAME))
                .WithMessage(ValidationCodes.TOO_LONG_USER_IMAGE_NAME);
        });

        When(command => command.UserVideoName != null, () =>
        {
            RuleFor(command => command.UserVideoName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.TOO_LONG_USER_VIDEO_NAME))
                .WithMessage(ValidationCodes.TOO_LONG_USER_VIDEO_NAME);
        });
    }
}