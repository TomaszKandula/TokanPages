using TokanPages.Backend.Shared.Resources;
using FluentValidation;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddVideoAssetCommandValidator : AbstractValidator<AddVideoAssetCommand>
{
    public AddVideoAssetCommandValidator(IOptions<AppSettingsModel> configuration)
    {
        var sizeLimit = configuration.Value.AzStorageMaxFileSizeSingleAsset;

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
