using FluentValidation.Results;

namespace TokanPages.Backend.Core.Models
{
    public sealed class ValidationError
    {
        public string PropertyName { get; }

        public string ErrorCode { get; }
        
        public string ErrorMessage { get; }

        public ValidationError(string APropertyName, string AErrorCode, string AErrorMessage = null)
        {
            PropertyName = APropertyName;
            ErrorCode = AErrorCode;
            ErrorMessage = AErrorMessage;
        }

        public ValidationError(ValidationFailure AValidationFailure) 
            : this(AValidationFailure.PropertyName, AValidationFailure.ErrorCode, AValidationFailure.ErrorMessage) { }
    }
}
