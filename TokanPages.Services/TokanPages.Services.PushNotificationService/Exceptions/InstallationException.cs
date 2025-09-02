using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Exceptions;

namespace TokanPages.Services.PushNotificationService.Exceptions;

[ExcludeFromCodeCoverage]
public class InstallationException : BusinessException
{
    public InstallationException(string errorMessage = "") : base(errorMessage) { }
}