using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions;

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