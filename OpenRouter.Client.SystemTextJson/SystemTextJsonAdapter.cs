using System;
using System.Text.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Basic implementation of ISerializer using System.Text.Json.
    /// </summary>
    /// <summary>
    /// Basic implementation of ISerializer using System.Text.Json.
    /// </summary>
    public class SystemTextJsonAdapter : ISerializer
    {
        /// <inheritdoc />
        public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
        /// <inheritdoc />
        public T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json)!;
        /// <inheritdoc />
        public object Deserialize(string json, Type type) => JsonSerializer.Deserialize(json, type)!;
    }
}
