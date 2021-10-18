namespace TokanPages.Backend.Core.Logger
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Serilog;

    /// <summary>
    /// Logger service that allows to store messages from application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Logger : ILogger
    {
        /// <summary>
        /// Log debug information to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="message">A debug message to store.</param>
        public void LogDebug(string message)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {message}");
            Log.Debug("{AMessage}", message);
        }

        /// <summary>
        /// Log information message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="message">An information message to store.</param>
        public void LogInformation(string message)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {message}");
            Log.Information("{AMessage}", message);
        }

        /// <summary>
        /// Log warning message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="message">A warning message to store.</param>
        public void LogWarning(string message)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {message}");
            Log.Warning("{AMessage}", message);
        }

        /// <summary>
        /// Log error message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="message">An error message to store.</param>
        public void LogError(string message)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {message}");
            Log.Error("{AMessage}", message);
        }

        /// <summary>
        /// Log critical error to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="message">A critical error message to store.</param>
        public void LogCriticalError(string message)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {message}");
            Log.Fatal("{AMessage}", message);
        }
    }
}