using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors that occur during OpenRouter API client operations.
    /// </summary>
    public class OpenRouterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterException"/> class.
        /// </summary>
        public OpenRouterException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
