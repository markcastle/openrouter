using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for errors occurring during client operations.
    /// </summary>
    public class ErrorEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets or sets the exception that occurred.
        /// </summary>
        public Exception? Exception { get; set; }
    }
}
