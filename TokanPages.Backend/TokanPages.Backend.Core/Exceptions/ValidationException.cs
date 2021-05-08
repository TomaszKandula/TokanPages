using FluentValidation.Results;

namespace TokanPages.Backend.Core.Exceptions
{
    public class ValidationException : BusinessException
    {
        public ValidationResult ValidationResult { get; }

        public ValidationException(ValidationResult AValidationResult) : base(CommonErrorCodes.VALIDATION_ERROR)
            => ValidationResult = AValidationResult;
    }
}
