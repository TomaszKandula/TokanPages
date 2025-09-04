using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class AccessException : BusinessException
{
    public AccessException(string errorCode, string errorMessage = "") 
        : base(errorCode, errorMessage) { }
}