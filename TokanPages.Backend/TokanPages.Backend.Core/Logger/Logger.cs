using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace TokanPages.Backend.Core.Logger
{
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
        /// <param name="AMessage">A debug message to store.</param>
        public void LogDebug(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Debug("{AMessage}", AMessage);
        }

        /// <summary>
        /// Log information message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="AMessage">An information message to store.</param>
        public void LogInformation(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Information("{AMessage}", AMessage);
        }

        /// <summary>
        /// Log warning message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="AMessage">A warning message to store.</param>
        public void LogWarning(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Warning("{AMessage}", AMessage);
        }

        /// <summary>
        /// Log error message to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="AMessage">An error message to store.</param>
        public void LogError(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Error("{AMessage}", AMessage);
        }

        /// <summary>
        /// Log critical error to storage with current datetime stamp.
        /// </summary>
        /// <remarks>
        /// Given message will also be printed in debug console.
        /// </remarks>
        /// <param name="AMessage">A critical error message to store.</param>
        public void LogCriticalError(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Fatal("{AMessage}", AMessage);
        }
    }
}
