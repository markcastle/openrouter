using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for errors occurring during client operations.
    /// </summary>
    public class ErrorEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the exception that occurred.
        /// </summary>
        public Exception Exception { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="ErrorEventArgs"/>.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        public ErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
