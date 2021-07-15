namespace TokanPages.Backend.Core.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation.Results;

    [ExcludeFromCodeCoverage]
    public sealed class ApplicationError
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorInnerMessage { get; set; }
        
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApplicationError(string AErrorCode, string AErrorMessage, string AErrorInnerMessage = "")
        {
            ErrorCode = AErrorCode;
            ErrorMessage = AErrorMessage;
            ErrorInnerMessage = AErrorInnerMessage;
        }

        public ApplicationError(string AErrorCode, string AErrorMessage, ValidationResult AValidationResult) : this(AErrorCode, AErrorMessage)
        {
            ValidationErrors = AValidationResult.Errors
                .Select(AValidationFailure => new ValidationError(AValidationFailure))
                .ToList();
        }
    }
}