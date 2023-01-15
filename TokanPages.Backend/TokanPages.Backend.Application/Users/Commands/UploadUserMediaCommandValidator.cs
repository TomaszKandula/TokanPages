using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadUserMediaCommandValidator : AbstractValidator<UploadUserMediaCommand>
{
    public UploadUserMediaCommandValidator(IConfiguration configuration)
    {
        var sizeLimit = configuration.GetValue<int>("AZ_Storage_MaxFileSizeUserMedia");

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