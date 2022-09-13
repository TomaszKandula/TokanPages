using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class BusinessException : Exception
{
    public string ErrorCode { get; } = "";

    protected BusinessException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public BusinessException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}