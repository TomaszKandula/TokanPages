using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserNoteCommandValidator : AbstractValidator<UpdateUserNoteCommand>
{
    public UpdateUserNoteCommandValidator(IConfiguration configuration)
    {
        var maxSize = configuration.GetValue<int>("UserNote_MaxSize");

        RuleFor(command => command.Note)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .Must(note => note.Length <= maxSize)
            .WithErrorCode(nameof(ValidationCodes.TOO_LONG_USER_NOTE))
            .WithMessage(ValidationCodes.TOO_LONG_USER_NOTE);
    }
}