using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class BusinessException : Exception
{
    public string ErrorCode { get; } = "";

    public BusinessException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}