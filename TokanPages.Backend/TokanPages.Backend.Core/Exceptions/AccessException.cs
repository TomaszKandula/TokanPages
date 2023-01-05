using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class AccessException : BusinessException
{
    protected AccessException(SerializationInfo serializationInfo, StreamingContext streamingContext) 
        : base(serializationInfo, streamingContext) { }

    public AccessException(string errorCode, string errorMessage = "") 
        : base(errorCode, errorMessage) { }
}