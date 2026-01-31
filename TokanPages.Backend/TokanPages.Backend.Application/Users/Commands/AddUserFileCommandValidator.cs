using FluentValidation;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommandValidator : AbstractValidator<AddUserFileCommand>
{
    public AddUserFileCommandValidator(IOptions<AppSettingsModel> configuration)
    {
        var sizeLimit = configuration.Value.AzStorageMaxFileSizeUserMedia;

        RuleFor(command => command.Type)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .IsInEnum()
            .WithErrorCode(nameof(ValidationCodes.INVALID_ENUM_VALUE))
            .WithMessage(ValidationCodes.INVALID_ENUM_VALUE);

        RuleFor(command => command.BinaryData)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        When(command => command.BinaryData != null, () =>
        {
            RuleFor(command => command.BinaryData)
                .Must(bytes => bytes!.Length <= sizeLimit)
                .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
                .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
        });
    }
}