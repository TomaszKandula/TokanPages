namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using Domain.Enums;
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

        RuleFor(command => command.MediaTarget)
            .IsInEnum()
            .NotEqual(UserMedia.NotSpecified)
            .WithErrorCode(nameof(ValidationCodes.NOT_SPECIFIED_MEDIA_TARGET))
            .WithMessage(ValidationCodes.NOT_SPECIFIED_MEDIA_TARGET);

        RuleFor(command => command.MediaName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.TOO_LONG_MEDIA_NAME))
            .WithMessage(ValidationCodes.TOO_LONG_MEDIA_NAME);

        RuleFor(command => command.MediaType)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.TOO_LONG_MEDIA_TYPE))
            .WithMessage(ValidationCodes.TOO_LONG_MEDIA_TYPE);

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