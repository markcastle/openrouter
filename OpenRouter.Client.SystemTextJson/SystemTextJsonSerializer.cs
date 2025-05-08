using System;
using System.Text.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Provides serialization and deserialization using System.Text.Json for OpenRouter models.
    /// </summary>
    public class SystemTextJsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTextJsonSerializer"/> class.
        /// </summary>
        /// <param name="options">Optional JsonSerializerOptions to customize serialization.</param>
        public SystemTextJsonSerializer(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        /// <inheritdoc/>
        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, _options);
        }

        /// <inheritdoc/>
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options)!;
        }

        /// <inheritdoc/>
        public object Deserialize(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, _options)!;
        }
    }
}
