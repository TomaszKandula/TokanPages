namespace TokanPages.Backend.Application.Logger.Queries;

using FluentValidation;
using Shared.Resources;

public class GetLogFileContentQueryValidator : AbstractValidator<GetLogFileContentQuery>
{
    public GetLogFileContentQueryValidator()
    {
        RuleFor(query => query.LogFileName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.NAME_TOO_LONG))
            .WithMessage(ValidationCodes.NAME_TOO_LONG);
    }
}