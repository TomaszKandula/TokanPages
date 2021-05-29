using System;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Exceptions
{
    [Serializable]
    public class ValidationException : BusinessException
    {
        public ValidationResult ValidationResult { get; }

        public ValidationException(ValidationResult AValidationResult, string AErrorMessage = "") : base(AErrorMessage)
            => ValidationResult = AValidationResult;
    }
}
