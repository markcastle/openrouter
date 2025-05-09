using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.NewtonsoftJson
{
    /// <summary>
    /// Provides serialization and deserialization using Newtonsoft.Json for OpenRouter models.
    /// </summary>
    /// <summary>
    /// Provides serialization and deserialization using Newtonsoft.Json for OpenRouter models.
    /// Wraps all serialization errors in <see cref="OpenRouterSerializationException"/> for consistency and clarity.
    /// </summary>
    public class NewtonsoftJsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;


        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class.
        /// By default, uses snake_case property naming for OpenRouter API compatibility.
        /// To use a custom naming policy or property mapping, provide a configured <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="settings">
        /// Optional <see cref="JsonSerializerSettings"/> to customize serialization, including property naming and custom converters.
        /// If null, the serializer will use <see cref="Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy"/> and ignore null values for OpenRouter compatibility.
        /// </param>
        public NewtonsoftJsonSerializer(JsonSerializerSettings? settings = null)
        {
            // Property naming is handled here, not in model attributes. By default, use snake_case for OpenRouter compatibility.
            _settings = settings ?? new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver {
                    NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class with custom converters.
        /// </summary>
        /// <param name="converters">A collection of custom JsonConverters to use.</param>
        public NewtonsoftJsonSerializer(IEnumerable<JsonConverter> converters)
        {
            _settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            if (converters != null)
            {
                foreach (var converter in converters)
                {
                    _settings.Converters.Add(converter);
                }
            }
        }

        /// <summary>
        /// Serializes the specified value to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The JSON string representation of the value.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public string Serialize<T>(T value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, _settings);
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Serialization failed.", ex);
            }
        }

        /// <summary>
        /// Deserializes the specified JSON string to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public T Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _settings)!;
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
                return JsonConvert.DeserializeObject(json, type, _settings)!;
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
        /// <param name="stream">The stream to write the JSON to.</param>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public void Serialize<T>(T value, System.IO.Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Target stream cannot be null.");
                }

                using var writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8, 1024, leaveOpen: true);
                var json = JsonConvert.SerializeObject(value, _settings);
                writer.Write(json);
                writer.Flush();
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
        /// <param name="stream">The stream to write the JSON to.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <exception cref="OpenRouterSerializationException">Thrown if serialization fails.</exception>
        public async System.Threading.Tasks.Task SerializeAsync<T>(T value, System.IO.Stream stream, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Target stream cannot be null.");
                }

                var json = JsonConvert.SerializeObject(value, _settings);
                await using var writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8, 1024, leaveOpen: true);
                await writer.WriteAsync(json.AsMemory(), cancellationToken).ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Streaming serialization failed.", ex);
            }
        }

        /// <summary>
        /// Deserializes an object of type T from the provided stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="stream">The stream to read the JSON from.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public T Deserialize<T>(System.IO.Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Source stream cannot be null.");
                }

                using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8, true, 1024, leaveOpen: true);
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json, _settings)!;
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
        /// <param name="stream">The stream to read the JSON from.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="OpenRouterSerializationException">Thrown if deserialization fails.</exception>
        public async System.Threading.Tasks.Task<T> DeserializeAsync<T>(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                if (stream == null)
                {
                    throw new OpenRouterSerializationException("Source stream cannot be null.");
                }

                using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8, true, 1024, leaveOpen: true);
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(json, _settings)!;
            }
            catch (Exception ex)
            {
                throw new OpenRouterSerializationException("Streaming deserialization failed.", ex);
            }
        }
    }
}
