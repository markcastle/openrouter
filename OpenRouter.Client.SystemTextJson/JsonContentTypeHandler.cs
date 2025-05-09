using System;
using System.Text;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Handles serialization and deserialization for application/json content type using the provided serializer abstraction.
    /// </summary>
    public class JsonContentTypeHandler : IContentTypeHandler
    {
        private readonly ISerializer _serializer;
        public string ContentType => "application/json";

        public JsonContentTypeHandler(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>
        /// Serializes the given object to a string for the specified content type.
        /// </summary>
        public string Serialize(object value, string contentType)
        {
            if (!string.Equals(contentType, ContentType, StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException($"Content type '{contentType}' is not supported by this handler.");
            if (value == null) return string.Empty;
            var result = _serializer.Serialize(value);
            return result ?? string.Empty;
        }

        /// <summary>
        /// Deserializes the given string to an object of the specified type.
        /// </summary>
        [return: System.Diagnostics.CodeAnalysis.MaybeNull]
        public object Deserialize(string content, Type type, string contentType)
        {
            if (!string.Equals(contentType, ContentType, StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException($"Content type '{contentType}' is not supported by this handler.");
            if (string.IsNullOrEmpty(content))
            {
                // Return default value for value types, null for reference types
                if (type.IsValueType)
                {
                    var defaultValue = Activator.CreateInstance(type);
                    return defaultValue;
                }
                return null;
            }
            var result = _serializer.Deserialize(content, type);
            return result;
        }
    }
}
