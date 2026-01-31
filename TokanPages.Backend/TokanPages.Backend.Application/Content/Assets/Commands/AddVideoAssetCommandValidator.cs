using TokanPages.Backend.Shared.Resources;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Configuration.Options;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddVideoAssetCommandValidator : AbstractValidator<AddVideoAssetCommand>
{
    public AddVideoAssetCommandValidator(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var sizeLimit = settings.AzStorageMaxFileSizeSingleAsset;

        RuleFor(command => command.BinaryData)
            .Must(bytes => bytes!.Length > 0)
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.BinaryData)
            .Must(bytes => bytes!.Length <= sizeLimit)
            .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
            .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
    }
}
