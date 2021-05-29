using System;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Exceptions
{
    [Serializable]
    public class ValidationException : BusinessException
    {
        public ValidationResult ValidationResult { get; }

        protected ValidationException(SerializationInfo ASerializationInfo, 
            StreamingContext AStreamingContext) : base(ASerializationInfo, AStreamingContext) { }

        public ValidationException(ValidationResult AValidationResult, string AErrorMessage = "") : base(AErrorMessage)
            => ValidationResult = AValidationResult;
    }
}
