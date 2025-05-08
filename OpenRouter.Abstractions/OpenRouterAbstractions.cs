//using System;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using OpenRouter.Abstractions;

//namespace OpenRouter.Abstractions
//{
//    // All public types have been moved to individual files for modularity and maintainability.
//    // This file is intentionally left as a placeholder for future barrel exports or documentation.
//}
//    /// <summary>
//    /// Options for serialization configuration.
//    /// </summary>
//    public class SerializationOptions : IValidatableOptions
//    {
//        /// <summary>
//        /// Gets or sets the serializer type ("SystemTextJson", "NewtonsoftJson").
//        /// </summary>
//        public string? SerializerType { get; set; } = "SystemTextJson";

//        /// <summary>
//        /// Gets or sets a value indicating whether to ignore null values during serialization.
//        /// </summary>
//        public bool IgnoreNullValues { get; set; } = true;

//        /// <inheritdoc/>
//        public void Validate()
//        {
//            if (string.IsNullOrWhiteSpace(SerializerType))
//            {
//                throw new OpenRouterSerializationException("SerializerType must be specified.");
//            }
//        }
//    }

//    /// <summary>
//    /// Options for resilience and retry configuration.
//    /// </summary>
//    public class ResilienceOptions : IValidatableOptions
//    {
//        /// <summary>
//        /// Gets or sets the retry policy type (e.g., "ExponentialBackoff").
//        /// </summary>
//        public string? RetryPolicy { get; set; } = "ExponentialBackoff";

//        /// <summary>
//        /// Gets or sets the circuit breaker threshold.
//        /// </summary>
//        public int CircuitBreakerThreshold { get; set; } = 5;

//        /// <summary>
//        /// Gets or sets the circuit breaker reset interval in seconds.
//        /// </summary>
//        public int CircuitBreakerResetSeconds { get; set; } = 60;

//        /// <inheritdoc/>
//        public void Validate()
//        {
//            if (CircuitBreakerThreshold < 0)
//            {
//                throw new OpenRouterException("CircuitBreakerThreshold cannot be negative.");
//            }

//            if (CircuitBreakerResetSeconds <= 0)
//            {
//                throw new OpenRouterException("CircuitBreakerResetSeconds must be positive.");
//            }
//        }
//    }

//    /// <summary>
//    /// Options for provider routing configuration.
//    /// </summary>
//    public class ProviderRoutingOptions : IValidatableOptions
//    {
//        /// <summary>
//        /// Gets or sets the preferred provider name (if any).
//        /// </summary>
//        public string? PreferredProvider { get; set; }

//        /// <summary>
//        /// Gets or sets a value indicating whether to enable provider fallback.
//        /// </summary>
//        public bool EnableFallback { get; set; } = true;

//        /// <inheritdoc/>
//        public void Validate()
//        {
//            // No required fields for now; add validation as needed
//        }
//    }

//    /// <summary>
//    /// Provides an event notification system for OpenRouter client operations.
//    /// </summary>
//    public interface IOpenRouterEventNotifier
//    {
//        /// <summary>
//        /// Occurs when a request is sent to the API.
//        /// </summary>
//        event EventHandler<RequestEventArgs>? RequestSent;

//        /// <summary>
//        /// Occurs when a response is received from the API.
//        /// </summary>
//        event EventHandler<ResponseEventArgs>? ResponseReceived;

//        /// <summary>
//        /// Occurs when a streaming update is received.
//        /// </summary>
//        event EventHandler<StreamingEventArgs>? StreamingUpdate;

//        /// <summary>
//        /// Occurs when an error occurs during a client operation.
//        /// </summary>
//        event EventHandler<ErrorEventArgs>? ErrorOccurred;

//        /// <summary>
//        /// Occurs to report progress of a long-running operation.
//        /// </summary>
//        event EventHandler<ProgressEventArgs>? ProgressChanged;
//    }

//    /// <summary>
//    /// Base class for all OpenRouter event arguments.
//    /// </summary>
//    public abstract class OpenRouterEventArgs : EventArgs
//    {
//        /// <summary>
//        /// Gets the UTC timestamp when the event occurred.
//        /// </summary>
//        public DateTime TimestampUtc { get; } = DateTime.UtcNow;
//    }

//    /// <summary>
//    /// Event args for request events.
//    /// </summary>
//    public class RequestEventArgs : OpenRouterEventArgs
//    {
//        /// <summary>
//        /// Gets the request model sent to the API.
//        /// </summary>
//        public IRequestModel Request { get; }

//        /// <summary>
//        /// Initializes a new instance of <see cref="RequestEventArgs"/>.
//        /// </summary>
//        /// <param name="request">The request model.</param>
//        public RequestEventArgs(IRequestModel request)
//        {
//            Request = request;
//        }
//    }

//    /// <summary>
//    /// Event args for response events.
//    /// </summary>
//    public class ResponseEventArgs : OpenRouterEventArgs
//    {
//        /// <summary>
//        /// Gets the response model received from the API.
//        /// </summary>
//        public IResponseModel Response { get; }

//        /// <summary>
//        /// Initializes a new instance of <see cref="ResponseEventArgs"/>.
//        /// </summary>
//        /// <param name="response">The response model.</param>
//        public ResponseEventArgs(IResponseModel response)
//        {
//            Response = response;
//        }
//    }

//    /// <summary>
//    /// Event args for streaming updates.
//    /// </summary>
//    public class StreamingEventArgs : OpenRouterEventArgs
//    {
//        /// <summary>
//        /// Gets the data received in the streaming update.
//        /// </summary>
//        public object Data { get; }

//        /// <summary>
//        /// Initializes a new instance of <see cref="StreamingEventArgs"/>.
//        /// </summary>
//        /// <param name="data">The streaming data.</param>
//        public StreamingEventArgs(object data)
//        {
//            Data = data;
//        }
//    }

//    /// <summary>
//    /// Event args for error events.
//    /// </summary>
//    public class ErrorEventArgs : OpenRouterEventArgs
//    {
//        /// <summary>
//        /// Gets the exception that occurred.
//        /// </summary>
//        public Exception Exception { get; }

//        /// <summary>
//        /// Initializes a new instance of <see cref="ErrorEventArgs"/>.
//        /// </summary>
//        /// <param name="exception">The exception that occurred.</param>
//        public ErrorEventArgs(Exception exception)
//        {
//            Exception = exception;
//        }
//    }

//    /// <summary>
//    /// Event args for progress notifications.
//    /// </summary>
//    public class ProgressEventArgs : OpenRouterEventArgs
//    {
//        /// <summary>
//        /// Gets the percentage of progress (0-100).
//        /// </summary>
//        public int ProgressPercentage { get; }

//        /// <summary>
//        /// Gets an optional status message.
//        /// </summary>
//        public string? StatusMessage { get; }

//        /// <summary>
//        /// Initializes a new instance of <see cref="ProgressEventArgs"/>.
//        /// </summary>
//        /// <param name="progressPercentage">The progress percentage (0-100).</param>
//        /// <param name="statusMessage">An optional status message.</param>
//        public ProgressEventArgs(int progressPercentage, string? statusMessage = null)
//        {
//            ProgressPercentage = progressPercentage;
//            StatusMessage = statusMessage;
//        }
//    }

//    /// <summary>
//    /// Provides serialization and deserialization services for OpenRouter models.
//    /// </summary>
//    public interface ISerializer
//    {
//        /// <summary>
//        /// Serializes the specified value to a string (e.g., JSON).
//        /// </summary>
//        /// <typeparam name="T">The type of the value to serialize.</typeparam>
//        /// <param name="value">The value to serialize.</param>
//        /// <returns>The serialized string representation.</returns>
//        string Serialize<T>(T value);

//        /// <summary>
//        /// Deserializes the specified string to a value of type <typeparamref name="T"/>.
//        /// </summary>
//        /// <typeparam name="T">The type to deserialize to.</typeparam>
//        /// <param name="json">The serialized string (e.g., JSON).</param>
//        /// <returns>The deserialized value.</returns>
//        T Deserialize<T>(string json);

//        /// <summary>
//        /// Deserializes the specified string to an object of the given type.
//        /// </summary>
//        /// <param name="json">The serialized string (e.g., JSON).</param>
//        /// <param name="type">The runtime type to deserialize to.</param>
//        /// <returns>The deserialized object.</returns>
//        object Deserialize(string json, Type type);
//    }

//    /// <summary>
//    /// Strongly-typed serializer for advanced scenarios.
//    /// </summary>
//    /// <typeparam name="T">The type to serialize/deserialize.</typeparam>
//    public interface ISerializer<T>
//    {
//        /// <summary>
//        /// Serializes the specified value to a string (e.g., JSON).
//        /// </summary>
//        /// <param name="value">The value to serialize.</param>
//        /// <returns>The serialized string representation.</returns>
//        string Serialize(T value);

//        /// <summary>
//        /// Deserializes the specified string to a value of type <typeparamref name="T"/>.
//        /// </summary>
//        /// <param name="json">The serialized string (e.g., JSON).</param>
//        /// <returns>The deserialized value.</returns>
//        T Deserialize(string json);
//    }
//}




