using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Errors;

[ExcludeFromCodeCoverage]
public sealed class ApplicationError
{
    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorInnerMessage { get; set; }

    public IEnumerable<ValidationError>? ValidationErrors { get; set; }

    public ApplicationError(string errorCode, string errorMessage, string errorInnerMessage = "")
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorInnerMessage = errorInnerMessage;
    }

    public ApplicationError(string errorCode, string errorMessage, ValidationResult validationResult) : this(errorCode, errorMessage)
    {
        ValidationErrors = validationResult.Errors
            .Select(validationFailure => new ValidationError(validationFailure))
            .ToList();
    }
}