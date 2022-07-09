namespace TokanPages.Backend.Core.Exceptions;

using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[Serializable]
public class AccessException : Exception
{
    public string ErrorCode { get; } = "";

    protected AccessException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public AccessException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}