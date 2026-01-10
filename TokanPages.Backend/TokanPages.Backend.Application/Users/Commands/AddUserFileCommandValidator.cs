using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommandValidator : AbstractValidator<AddUserFileCommand>
{
    public AddUserFileCommandValidator(IConfiguration configuration)
    {
        var sizeLimit = configuration.GetValue<int>("AZ_Storage_MaxFileSizeUserMedia");

        RuleFor(command => command.Type)
            .IsInEnum()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

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