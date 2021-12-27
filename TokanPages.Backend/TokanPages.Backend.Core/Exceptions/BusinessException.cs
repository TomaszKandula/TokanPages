namespace TokanPages.Backend.Core.Exceptions;

using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[Serializable]
public class BusinessException : Exception
{
    public string ErrorCode { get; }

    protected BusinessException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public BusinessException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}