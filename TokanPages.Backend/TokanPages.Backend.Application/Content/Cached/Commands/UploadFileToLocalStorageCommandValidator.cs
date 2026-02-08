using FluentValidation;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class UploadFileToLocalStorageCommandValidator : AbstractValidator<UploadFileToLocalStorageCommand>
{
    public UploadFileToLocalStorageCommandValidator(IOptions<AppSettingsModel> configuration)
    {
        var sizeLimit = configuration.Value.AzStorageMaxFileSizeSingleAsset;

        RuleFor(command => command.BinaryData)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        When(command => command.BinaryData != null, () =>
        {
            RuleFor(command => command.BinaryData)
                .Must(bytes => bytes!.Length > 0)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(command => command.BinaryData)
                .Must(bytes => bytes!.Length <= sizeLimit)
                .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
                .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
        });
    }
}