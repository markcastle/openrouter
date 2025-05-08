using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors returned by the OpenRouter API (e.g., HTTP 4xx/5xx).
    /// </summary>
    public class OpenRouterApiException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterApiException"/> class.
        /// </summary>
        public OpenRouterApiException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterApiException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterApiException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}
