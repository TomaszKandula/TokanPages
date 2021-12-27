namespace TokanPages.Backend.Core.Exceptions;

using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

[ExcludeFromCodeCoverage]
[Serializable]
public class ValidationException : BusinessException
{
    public ValidationResult ValidationResult { get; }

    protected ValidationException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public ValidationException(ValidationResult validationResult, string errorMessage = "") : base(errorMessage)
        => ValidationResult = validationResult;
}