using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Abstraction for logging within the OpenRouter client ecosystem.
    /// </summary>
    public interface IOpenRouterLogger
    {
        /// <summary>
        /// Logs a message with the specified log level and optional exception.
        /// </summary>
        /// <param name="level">The severity level of the log message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception related to the log entry, if any.</param>
        void Log(LogLevel level, string message, Exception? exception = null);
    }
}
