using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using TokanPages.Backend.Core.Exceptions;

namespace TokanPages.Services.PushNotificationService.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class InstallationException : BusinessException
{
    protected InstallationException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public InstallationException(string errorMessage = "") : base(errorMessage) { }
}