namespace TokanPages.HostedServices.Services.Abstractions;

/// <summary>
/// Contract for processing hosted service. 
/// </summary>
public interface IBaseProcessor
{
    /// <summary>
    /// Message handler registering.
    /// </summary>
    /// <returns></returns>
    Task RegisterOnMessageHandlerAndReceiveMessages();

    /// <summary>
    /// Queue closing.
    /// </summary>
    /// <returns></returns>
    Task CloseQueueAsync();

    /// <summary>
    /// Disposing.
    /// </summary>
    /// <returns></returns>
    ValueTask DisposeAsync();
}