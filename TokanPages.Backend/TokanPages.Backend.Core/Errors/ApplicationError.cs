using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace TokanPages.Backend.Core.Errors;

[ExcludeFromCodeCoverage]
public sealed class ApplicationError
{
    [JsonProperty("errorCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }

    [JsonProperty("errorInnerMessage")]
    public string ErrorInnerMessage { get; set; }

    [JsonProperty("validationErrors")]
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