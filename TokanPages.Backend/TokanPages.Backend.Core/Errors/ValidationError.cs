using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace TokanPages.Backend.Core.Errors;

[ExcludeFromCodeCoverage]
public sealed class ValidationError
{
    [JsonProperty("propertyName")]
    public string PropertyName { get; }

    [JsonProperty("errorCode")]
    public string ErrorCode { get; }

    [JsonProperty("errorMessage")]
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