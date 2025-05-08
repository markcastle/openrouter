using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors that occur when an API request times out.
    /// </summary>
    public class OpenRouterTimeoutException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterTimeoutException"/> class.
        /// </summary>
        public OpenRouterTimeoutException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterTimeoutException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterTimeoutException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterTimeoutException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterTimeoutException(string message, Exception innerException) : base(message, innerException) { }
    }
}
