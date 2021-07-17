namespace TokanPages.Backend.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation.Results;

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