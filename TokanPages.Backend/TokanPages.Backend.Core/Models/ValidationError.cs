namespace TokanPages.Backend.Core.Models;

using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

[ExcludeFromCodeCoverage]
public sealed class ValidationError
{
    public string PropertyName { get; }

    public string ErrorCode { get; }
        
    public string ErrorMessage { get; }

    public ValidationError(string propertyName, string errorCode, string errorMessage = null)
    {
        PropertyName = propertyName;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public ValidationError(ValidationFailure validationFailure) 
        : this(validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.ErrorMessage) { }
}