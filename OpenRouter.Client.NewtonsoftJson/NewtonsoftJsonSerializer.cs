using System;
using Newtonsoft.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.NewtonsoftJson
{
    /// <summary>
    /// Provides serialization and deserialization using Newtonsoft.Json for OpenRouter models.
    /// </summary>
    public class NewtonsoftJsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class.
        /// </summary>
        /// <param name="settings">Optional JsonSerializerSettings to customize serialization.</param>
        public NewtonsoftJsonSerializer(JsonSerializerSettings? settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        }

        /// <inheritdoc/>
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        /// <inheritdoc/>
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings)!;
        }

        /// <inheritdoc/>
        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, _settings)!;
        }
    }
}
