using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace OpenRouter.Abstractions
{
        /// <summary>
    /// Abstraction for the main OpenRouter API Client.
    /// </summary>
    public interface IOpenRouterClient
    {
        /// <summary>
        /// Sends a request to the OpenRouter API and returns the response asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <typeparam name="TResponse">The type of the response object.</typeparam>
        /// <param name="request">The request payload to send.</param>
        /// <param name="cancellationToken">A cancellation token for the async operation.</param>
        /// <returns>The response from the API.</returns>
        Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);
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

    /// <summary>
    /// Specifies log severity levels.
    /// </summary>
    /// <summary>
    /// Specifies log severity levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Logs that contain the most detailed messages. These messages may contain sensitive application data.
        /// </summary>
        Trace,
        /// <summary>
        /// Logs that are used for interactive investigation during development. These logs may contain sensitive application data.
        /// </summary>
        Debug,
        /// <summary>
        /// Logs that track the general flow of the application.
        /// </summary>
        Information,
        /// <summary>
        /// Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application to stop.
        /// </summary>
        Warning,
        /// <summary>
        /// Logs that highlight when the current flow of execution is stopped due to a failure.
        /// </summary>
        Error,
        /// <summary>
        /// Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.
        /// </summary>
        Critical
    }

    /// <summary>
    /// Represents the contract for all OpenRouter API request models.
    /// </summary>
    public interface IRequestModel
    {
        // Marker for request models; extend as needed for validation, metadata, etc.
    }

    /// <summary>
    /// Represents the contract for all OpenRouter API response models.
    /// </summary>
    public interface IResponseModel
    {
        // Marker for response models; extend as needed for status, metadata, etc.
    }

    /// <summary>
    /// Provides event notification for OpenRouter client operations.
    /// </summary>
    public interface IEventNotifier
    {
        /// <summary>
        /// Publishes an event to all registered subscribers.
        /// </summary>
        /// <typeparam name="TEvent">The type of event data.</typeparam>
        /// <param name="eventData">The event data to publish.</param>
        void Publish<TEvent>(TEvent eventData);
    }

    /// <summary>
    /// Provides configuration options for the OpenRouter client.
    /// </summary>
    public interface IOpenRouterConfiguration
    {
        /// <summary>
        /// Gets the base URL for the OpenRouter API.
        /// </summary>
        string BaseUrl { get; }
        // Add other configuration properties as needed (e.g., timeouts, auth, etc.)
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
