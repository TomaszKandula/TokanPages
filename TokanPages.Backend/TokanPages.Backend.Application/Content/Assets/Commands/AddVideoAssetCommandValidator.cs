using TokanPages.Backend.Shared.Resources;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddVideoAssetCommandValidator : AbstractValidator<AddVideoAssetCommand>
{
    public AddVideoAssetCommandValidator(IConfiguration configuration)
    {
        var sizeLimit = configuration.GetValue<int>("AZ_Storage_MaxFileSizeSingleAsset");

        RuleFor(command => command.BinaryData)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.BinaryData)
            .Must(bytes => bytes!.Length <= sizeLimit)
            .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
            .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
    }
}
