using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class AuthorizationException : BusinessException
{
    public AuthorizationException(string errorCode, string errorMessage = "") 
        : base(errorCode, errorMessage) { }
}