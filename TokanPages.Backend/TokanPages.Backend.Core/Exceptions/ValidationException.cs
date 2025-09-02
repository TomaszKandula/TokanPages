using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationException : BusinessException
{
    public ValidationResult ValidationResult { get; }

    public ValidationException(ValidationResult validationResult, string errorMessage = "") : base(errorMessage)
        => ValidationResult = validationResult;
}