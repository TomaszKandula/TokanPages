using System.Linq;
using System.Collections.Generic;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Models
{
    public sealed class ApplicationError
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApplicationError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public ApplicationError(string AErrorCode, string AErrorMessage, ValidationResult AValidationResult) : this(AErrorCode, AErrorMessage)
        {
            ValidationErrors = AValidationResult.Errors.Select(AError => new ValidationError(AError)).ToList();
        }
    }
}
