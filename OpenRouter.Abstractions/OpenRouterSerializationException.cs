using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents errors that occur during serialization or deserialization of API models.
    /// </summary>
    public class OpenRouterSerializationException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterSerializationException"/> class.
        /// </summary>
        public OpenRouterSerializationException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterSerializationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterSerializationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterSerializationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterSerializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
