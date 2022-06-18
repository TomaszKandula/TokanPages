namespace TokanPages.Backend.Core.Exceptions;

using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

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