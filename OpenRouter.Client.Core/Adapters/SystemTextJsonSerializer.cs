using System;
using System.Text.Json;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.Core.Adapters
{
    /// <summary>
    /// Basic implementation of ISerializer using System.Text.Json.
    /// </summary>
    public class SystemTextJsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
        public T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json)!;
        public object Deserialize(string json, Type type) => JsonSerializer.Deserialize(json, type)!;
    }
}
