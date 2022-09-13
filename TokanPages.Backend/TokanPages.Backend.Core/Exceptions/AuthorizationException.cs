using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class AuthorizationException : Exception
{
    public string ErrorCode { get; } = "";

    protected AuthorizationException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public AuthorizationException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}