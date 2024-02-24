using Azure.Messaging.ServiceBus;

namespace TokanPages.HostedServices.Services.Abstractions;

/// <summary>
/// Processing contract.
/// </summary>
public interface IProcessing : IBaseProcessor
{
    /// <summary>
    /// Process received data.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task ProcessMessagesAsync(ProcessMessageEventArgs args);

    /// <summary>
    /// Invoked on error event.
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    Task ProcessErrorAsync(ProcessErrorEventArgs arg);
}