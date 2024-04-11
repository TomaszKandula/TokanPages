using FluentValidation;
using TokanPages.Backend.Shared.Resources;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator(IConfiguration configuration)
    {
        var sizeLimit = configuration.GetValue<int>("AZ_Storage_MaxFileSizeUserMedia");

        When(command => command.BinaryData != null, () =>
        {
            RuleFor(command => command.BinaryData)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(command => command.BinaryData)
                .Must(bytes => bytes!.Length <= sizeLimit)
                .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
                .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
        });
    }
}