using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace OpenRouter.Abstractions
{
        /// <summary>
    /// Abstraction for the main OpenRouter API Client.
    /// </summary>
    public interface IOpenRouterClient
    {
        // TODO: Define core API operations
    }

    /// <summary>
    /// Abstraction for the HTTP transport layer, allowing for custom implementations and testing.
    /// </summary>
    public interface IHttpTransport
    {
        /// <summary>
        /// Sends an HTTP request asynchronously.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <returns>The HTTP response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }

    /// <summary>
    /// Provides authentication services for OpenRouter API requests.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Adds authentication headers or tokens to an outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message to modify.</param>
        void Authenticate(HttpRequestMessage request);
    }

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
