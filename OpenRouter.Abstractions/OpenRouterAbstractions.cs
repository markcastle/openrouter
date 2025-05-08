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

    /// <summary>
    /// Represents errors that occur due to network issues during API operations.
    /// </summary>
    public class OpenRouterNetworkException : OpenRouterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterNetworkException"/> class.
        /// </summary>
        public OpenRouterNetworkException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterNetworkException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenRouterNetworkException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterNetworkException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OpenRouterNetworkException(string message, Exception innerException) : base(message, innerException) { }
    }

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

    /// <summary>
    /// Provides a contract for validating options.
    /// </summary>
    public interface IValidatableOptions
    {
        /// <summary>
        /// Validates the options and throws an exception if invalid.
        /// </summary>
        void Validate();
    }

    /// <summary>
    /// Root options for configuring the OpenRouter client.
    /// </summary>
    public class OpenRouterClientOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the authentication options.
        /// </summary>
        public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();

        /// <summary>
        /// Gets or sets the HTTP configuration options.
        /// </summary>
        public HttpOptions Http { get; set; } = new HttpOptions();

        /// <summary>
        /// Gets or sets the serialization options.
        /// </summary>
        public SerializationOptions Serialization { get; set; } = new SerializationOptions();

        /// <summary>
        /// Gets or sets the resilience options.
        /// </summary>
        public ResilienceOptions Resilience { get; set; } = new ResilienceOptions();

        /// <summary>
        /// Gets or sets the provider routing options.
        /// </summary>
        public ProviderRoutingOptions ProviderRouting { get; set; } = new ProviderRoutingOptions();

        /// <inheritdoc/>
        public void Validate()
        {
            Authentication?.Validate();
            Http?.Validate();
            Serialization?.Validate();
            Resilience?.Validate();
            ProviderRouting?.Validate();
        }
    }

    /// <summary>
    /// Options for authentication configuration.
    /// </summary>
    public class AuthenticationOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the API key for authenticating requests.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the bearer token for authentication.
        /// </summary>
        public string? BearerToken { get; set; }

        /// <summary>
        /// Gets or sets the authentication scheme ("ApiKey", "Bearer", etc).
        /// </summary>
        public string? Scheme { get; set; }

        /// <inheritdoc/>
        public void Validate()
        {
            // At least one authentication method must be provided
            if (string.IsNullOrWhiteSpace(ApiKey) && string.IsNullOrWhiteSpace(BearerToken))
                throw new OpenRouterAuthenticationException("Either ApiKey or BearerToken must be provided.");
        }
    }

    /// <summary>
    /// Options for HTTP configuration.
    /// </summary>
    public class HttpOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the base URL for the API.
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the request timeout in seconds.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 100;

        /// <summary>
        /// Gets or sets the maximum number of retries for transient errors.
        /// </summary>
        public int MaxRetries { get; set; } = 3;

        /// <inheritdoc/>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new OpenRouterException("BaseUrl must be provided in HttpOptions.");
            if (TimeoutSeconds <= 0)
                throw new OpenRouterException("TimeoutSeconds must be positive.");
            if (MaxRetries < 0)
                throw new OpenRouterException("MaxRetries cannot be negative.");
        }
    }

    /// <summary>
    /// Options for serialization configuration.
    /// </summary>
    public class SerializationOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the serializer type ("SystemTextJson", "NewtonsoftJson").
        /// </summary>
        public string? SerializerType { get; set; } = "SystemTextJson";

        /// <summary>
        /// Gets or sets a value indicating whether to ignore null values during serialization.
        /// </summary>
        public bool IgnoreNullValues { get; set; } = true;

        /// <inheritdoc/>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SerializerType))
                throw new OpenRouterSerializationException("SerializerType must be specified.");
        }
    }

    /// <summary>
    /// Options for resilience and retry configuration.
    /// </summary>
    public class ResilienceOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the retry policy type (e.g., "ExponentialBackoff").
        /// </summary>
        public string? RetryPolicy { get; set; } = "ExponentialBackoff";

        /// <summary>
        /// Gets or sets the circuit breaker threshold.
        /// </summary>
        public int CircuitBreakerThreshold { get; set; } = 5;

        /// <summary>
        /// Gets or sets the circuit breaker reset interval in seconds.
        /// </summary>
        public int CircuitBreakerResetSeconds { get; set; } = 60;

        /// <inheritdoc/>
        public void Validate()
        {
            if (CircuitBreakerThreshold < 0)
                throw new OpenRouterException("CircuitBreakerThreshold cannot be negative.");
            if (CircuitBreakerResetSeconds <= 0)
                throw new OpenRouterException("CircuitBreakerResetSeconds must be positive.");
        }
    }

    /// <summary>
    /// Options for provider routing configuration.
    /// </summary>
    public class ProviderRoutingOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the preferred provider name (if any).
        /// </summary>
        public string? PreferredProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable provider fallback.
        /// </summary>
        public bool EnableFallback { get; set; } = true;

        /// <inheritdoc/>
        public void Validate()
        {
            // No required fields for now; add validation as needed
        }
    }

    /// <summary>
    /// Provides an event notification system for OpenRouter client operations.
    /// </summary>
    public interface IOpenRouterEventNotifier
    {
        /// <summary>
        /// Occurs when a request is sent to the API.
        /// </summary>
        event EventHandler<RequestEventArgs>? RequestSent;

        /// <summary>
        /// Occurs when a response is received from the API.
        /// </summary>
        event EventHandler<ResponseEventArgs>? ResponseReceived;

        /// <summary>
        /// Occurs when a streaming update is received.
        /// </summary>
        event EventHandler<StreamingEventArgs>? StreamingUpdate;

        /// <summary>
        /// Occurs when an error occurs during a client operation.
        /// </summary>
        event EventHandler<ErrorEventArgs>? ErrorOccurred;

        /// <summary>
        /// Occurs to report progress of a long-running operation.
        /// </summary>
        event EventHandler<ProgressEventArgs>? ProgressChanged;
    }

    /// <summary>
    /// Base class for all OpenRouter event arguments.
    /// </summary>
    public abstract class OpenRouterEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the UTC timestamp when the event occurred.
        /// </summary>
        public DateTime TimestampUtc { get; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Event args for request events.
    /// </summary>
    public class RequestEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the request model sent to the API.
        /// </summary>
        public IRequestModel Request { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="RequestEventArgs"/>.
        /// </summary>
        /// <param name="request">The request model.</param>
        public RequestEventArgs(IRequestModel request)
        {
            Request = request;
        }
    }

    /// <summary>
    /// Event args for response events.
    /// </summary>
    public class ResponseEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the response model received from the API.
        /// </summary>
        public IResponseModel Response { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ResponseEventArgs"/>.
        /// </summary>
        /// <param name="response">The response model.</param>
        public ResponseEventArgs(IResponseModel response)
        {
            Response = response;
        }
    }

    /// <summary>
    /// Event args for streaming updates.
    /// </summary>
    public class StreamingEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the data received in the streaming update.
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="StreamingEventArgs"/>.
        /// </summary>
        /// <param name="data">The streaming data.</param>
        public StreamingEventArgs(object data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Event args for error events.
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

    /// <summary>
    /// Event args for progress notifications.
    /// </summary>
    public class ProgressEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the percentage of progress (0-100).
        /// </summary>
        public int ProgressPercentage { get; }

        /// <summary>
        /// Gets an optional status message.
        /// </summary>
        public string? StatusMessage { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProgressEventArgs"/>.
        /// </summary>
        /// <param name="progressPercentage">The progress percentage (0-100).</param>
        /// <param name="statusMessage">An optional status message.</param>
        public ProgressEventArgs(int progressPercentage, string? statusMessage = null)
        {
            ProgressPercentage = progressPercentage;
            StatusMessage = statusMessage;
        }
    }
}



