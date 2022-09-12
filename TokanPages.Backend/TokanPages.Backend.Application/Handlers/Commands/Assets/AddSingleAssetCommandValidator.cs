namespace TokanPages.Backend.Application.Handlers.Commands.Assets;

using Shared.Services;
using Shared.Resources;
using FluentValidation;

public class AddSingleAssetCommandValidator : AbstractValidator<AddSingleAssetCommand>
{
    public AddSingleAssetCommandValidator(IApplicationSettings applicationSettings)
    {
        var sizeLimit = applicationSettings.AzureStorage.MaxFileSizeSingleAsset;

        RuleFor(command => command.MediaName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_100))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_100);

        RuleFor(command => command.MediaType)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_100))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_100);

        RuleFor(command => command.Data)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Data)
            .Must(bytes => bytes.Length <= sizeLimit)
            .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
            .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
    }
}