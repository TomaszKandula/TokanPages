using Azure.Messaging.ServiceBus;
using TokanPages.HostedServices.Abstractions;

namespace TokanPages.HostedServices.Base.Abstractions;

/// <summary>
/// Processing contract.
/// </summary>
public interface IProcessing : IBaseProcessor
{
    /// <summary>
    /// Seeks the given message ID in the 'ServiceBusMessages' table. If found, then deletes it and returns true.
    /// </summary>
    /// <param name="messageId">Message ID created before an event is sent.</param>
    /// <returns>True if message is found, otherwise false.</returns>
    Task<bool> CanContinue(Guid messageId);

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