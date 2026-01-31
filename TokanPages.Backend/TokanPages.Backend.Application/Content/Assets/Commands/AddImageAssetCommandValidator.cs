using TokanPages.Backend.Shared.Resources;
using FluentValidation;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddImageAssetCommandValidator : AbstractValidator<AddImageAssetCommand>
{
    public AddImageAssetCommandValidator(IOptions<AppSettingsModel> configuration)
    {
        var sizeLimit = configuration.Value.AzStorageMaxFileSizeSingleAsset;

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