namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using Shared.Services;
using Shared.Resources;
using FluentValidation;

public class UploadUserMediaCommandValidator : AbstractValidator<UploadUserMediaCommand>
{
    public UploadUserMediaCommandValidator(IApplicationSettings applicationSettings)
    {
        var sizeLimit = applicationSettings.AzureStorage.MaxFileSizeUserMedia;

        When(command => command.UserId != null, () =>
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });

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