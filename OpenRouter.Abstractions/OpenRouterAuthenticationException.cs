using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors that occur during authentication for the OpenRouter API.
    /// </summary>
    public class OpenRouterAuthenticationException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterAuthenticationException"/> class.
        /// </summary>
        public OpenRouterAuthenticationException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterAuthenticationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterAuthenticationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterAuthenticationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterAuthenticationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
