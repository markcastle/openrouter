using System;
using System.Text;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Handles serialization and deserialization for application/json content type using the provided serializer abstraction.
    /// </summary>
    /// <summary>
    /// Handles serialization and deserialization for application/json content type using the provided serializer abstraction.
    /// </summary>
    public class JsonContentTypeHandler : IContentTypeHandler
    {
        private readonly ISerializer _serializer;
        /// <summary>
        /// The supported content type.
        /// </summary>
        public string ContentType => "application/json";

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContentTypeHandler"/> class.
        /// </summary>
        /// <param name="serializer">The serializer to use.</param>
        public JsonContentTypeHandler(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>
        /// Serializes the given object to a string for the specified content type.
        /// </summary>
        /// <summary>
        /// Serializes the given object to a string for the specified content type.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="contentType">The content type.</param>
        /// <returns>The serialized string.</returns>
        public string Serialize(object value, string contentType)
        {
            if (!string.Equals(contentType, ContentType, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException($"Content type '{contentType}' is not supported by this handler.");
            }

            if (value == null)
            {
                return string.Empty;
            }

            var result = _serializer.Serialize(value);
            return result ?? string.Empty;
        }

        /// <summary>
        /// Deserializes the given string to an object of the specified type.
        /// </summary>
        /// <param name="content">The content string to deserialize.</param>
        /// <param name="type">The target type.</param>
        /// <param name="contentType">The content type.</param>
        /// <returns>The deserialized object.</returns>
        [return: System.Diagnostics.CodeAnalysis.MaybeNull]
        public object Deserialize(string content, Type type, string contentType)
        {
            if (!string.Equals(contentType, ContentType, StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException($"Content type '{contentType}' is not supported by this handler.");
            }

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
