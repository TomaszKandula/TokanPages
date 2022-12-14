using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Errors;

[ExcludeFromCodeCoverage]
public sealed class ValidationError
{
    public string PropertyName { get; }

    public string ErrorCode { get; }

    public string ErrorMessage { get; }

    public ValidationError(string propertyName, string errorCode, string errorMessage = "")
    {
        PropertyName = propertyName;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public ValidationError(ValidationFailure validationFailure) 
        : this(validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.ErrorMessage) { }
}