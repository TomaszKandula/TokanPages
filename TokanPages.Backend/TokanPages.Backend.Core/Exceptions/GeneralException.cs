using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class GeneralException : BusinessException
{
    protected GeneralException(SerializationInfo serializationInfo, StreamingContext streamingContext) 
        : base(serializationInfo, streamingContext) { }

    public GeneralException(string errorCode, string errorMessage = "") 
        : base(errorCode, errorMessage) { }
}