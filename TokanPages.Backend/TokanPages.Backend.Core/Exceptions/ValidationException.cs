using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
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
