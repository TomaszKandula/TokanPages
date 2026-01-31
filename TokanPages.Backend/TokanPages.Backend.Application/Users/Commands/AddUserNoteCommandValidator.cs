using FluentValidation;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandValidator : AbstractValidator<AddUserNoteCommand>
{
    public AddUserNoteCommandValidator(IConfiguration configuration)
    {
        var settings = configuration.GetSettings();
        var maxSize = settings.UserNoteMaxSize;

        RuleFor(command => command.Note)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .Must(note => note.Length <= maxSize)
            .WithErrorCode(nameof(ValidationCodes.TOO_LONG_USER_NOTE))
            .WithMessage(ValidationCodes.TOO_LONG_USER_NOTE);
    }
}