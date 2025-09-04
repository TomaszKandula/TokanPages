using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class GeneralException : BusinessException
{
    public GeneralException(string errorCode, string errorMessage = "") 
        : base(errorCode, errorMessage) { }
}