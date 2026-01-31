using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class UploadFileToLocalStorageCommandValidator : AbstractValidator<UploadFileToLocalStorageCommand>
{
    public UploadFileToLocalStorageCommandValidator(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var sizeLimit = settings.AzStorageMaxFileSizeSingleAsset;

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