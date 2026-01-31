using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Logger.Commands;

public class UploadLogFileCommandValidator : AbstractValidator<UploadLogFileCommand>
{
    public UploadLogFileCommandValidator(IConfiguration configuration)
    {
        var settings = configuration.GetSettings();
        var sizeLimit = settings.AzStorageMaxFileSizeUserMedia;

        RuleFor(command => command.CatalogName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(225)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_225);

        RuleFor(command => command.Data)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Data)
            .Must(bytes => bytes!.Length > 0 && bytes.Length <= sizeLimit)
            .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
            .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
    }
}