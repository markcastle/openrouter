using System;
using System.Text.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Provides serialization and deserialization using System.Text.Json for OpenRouter models.
    /// 
    /// <para><b>Performance Best Practices:</b></para>
    /// <list type="bullet">
    /// <item>Reuse a single instance of <see cref="JsonSerializerOptions"/> for all serialization operations.</item>
    /// <item>Register custom converters and naming policies in the options constructor.</item>
    /// <item>For large payloads, consider using streams and async methods to minimize memory usage.</item>
    /// <item>Advanced: You can provide pooled memory or buffer settings via <see cref="JsonSerializerOptions"/> for further tuning.</item>
    /// </list>
    /// </summary>
    public class SystemTextJsonSerializer : ISerializer
    {
        // JsonSerializerOptions is constructed once and reused for all operations to maximize performance.
        // Register custom converters and policies here to avoid repeated allocations.
        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTextJsonSerializer"/> class.
        /// By default, uses snake_case property naming for OpenRouter API compatibility. To use a custom naming policy or property mapping, provide a configured <see cref="JsonSerializerOptions"/>.
        /// </summary>
        /// <param name="options">Optional <see cref="JsonSerializerOptions"/> to customize serialization, including property naming and custom converters. If null, snake_case is used by default.</param>
        public SystemTextJsonSerializer(JsonSerializerOptions? options = null)
        {
            // Property naming is handled here, not in model attributes. By default, use snake_case for OpenRouter compatibility.
            _options = options ?? new JsonSerializerOptions { PropertyNamingPolicy = new SnakeCaseJsonNamingPolicy() };
        }

        /// <inheritdoc/>
        /// <summary>
        /// Serializes the specified value to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The JSON string representation of the value.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public string Serialize<T>(T value)
        {
            try
            {
                // System.Text.Json returns "null" for null values
                return JsonSerializer.Serialize(value, _options);
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Serialization failed.", ex);
            }
        }

        /// <summary>
        /// Deserializes the specified JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public T Deserialize<T>(string json)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new OpenRouterSerializationException("Cannot deserialize from null or empty JSON string.");
                }

                var result = JsonSerializer.Deserialize<T>(json, _options);
                if (result == null)
                {
                    throw new OpenRouterSerializationException($"Deserialization returned null for type {typeof(T).Name}.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Deserialization failed.", ex);
            }
        }

        /// <summary>
        /// Deserializes the specified JSON string to an object of the specified type.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="type">The type to deserialize to.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public object Deserialize(string json, Type type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new OpenRouterSerializationException("Cannot deserialize from null or empty JSON string.");
                }

                if (type == null)
                {
                    throw new OpenRouterSerializationException("Target type must be provided for deserialization.");
                }

                var result = JsonSerializer.Deserialize(json, type, _options);
                if (result == null)
                {
                    throw new OpenRouterSerializationException($"Deserialization returned null for type {type.Name}.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Deserialization failed.", ex);
            }
        }
        /// <summary>
        /// Serializes the specified value to the provided stream.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="stream">The target stream.</param>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public void Serialize<T>(T value, System.IO.Stream stream)
        {
            try
            {
                if (value == null)
                {
                    throw new OpenRouterSerializationException("Cannot serialize null value.");
                }

                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Target stream cannot be null.");
                }

                JsonSerializer.Serialize(stream, value, _options);
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Streaming serialization failed.", ex);
            }
        }

        /// <summary>
        /// Asynchronously serializes the specified value to the provided stream.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="stream">The target stream.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public async System.Threading.Tasks.Task SerializeAsync<T>(T value, System.IO.Stream stream, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                if (value == null)
                {
                    throw new OpenRouterSerializationException("Cannot serialize null value.");
                }

                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Target stream cannot be null.");
                }

                await JsonSerializer.SerializeAsync(stream, value, _options, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Async streaming serialization failed.", ex);
            }
        }

        /// <summary>
        /// Deserializes an object of type T from the provided stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="stream">The source stream.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public T Deserialize<T>(System.IO.Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Source stream cannot be null.");
                }

                var result = JsonSerializer.Deserialize<T>(stream, _options);
                if (result == null)
                {
                    throw new OpenRouterSerializationException($"Deserialization returned null for type {typeof(T).Name}.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Streaming deserialization failed.", ex);
            }
        }

        /// <summary>
        /// Asynchronously deserializes an object of type T from the provided stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="stream">The source stream.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public async System.Threading.Tasks.Task<T> DeserializeAsync<T>(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Source stream cannot be null.");
                }

                var result = await JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken).ConfigureAwait(false);
                if (result == null)
                {
                    throw new OpenRouterSerializationException($"Deserialization returned null for type {typeof(T).Name}.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Async streaming deserialization failed.", ex);
            }
        }
    }
}
