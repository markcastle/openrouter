using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors that occur when the API rate limit is exceeded.
    /// </summary>
    public class OpenRouterRateLimitException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterRateLimitException"/> class.
        /// </summary>
        public OpenRouterRateLimitException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterRateLimitException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterRateLimitException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterRateLimitException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterRateLimitException(string message, Exception innerException) : base(message, innerException) { }
    }
}
