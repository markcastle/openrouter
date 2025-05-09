using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Handles content type-specific serialization and deserialization.
    /// </summary>
    public interface IContentTypeHandler
    {
        /// <summary>
        /// Serializes the given object to a string for the specified content type.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="contentType">The target content type.</param>
        /// <returns>The serialized string.</returns>
        string Serialize(object value, string contentType);

        /// <summary>
        /// Deserializes the given string to an object of the specified type.
        /// </summary>
        /// <param name="content">The content string.</param>
        /// <param name="type">The target type.</param>
        /// <param name="contentType">The content type.</param>
        /// <returns>The deserialized object.</returns>
        [return: System.Diagnostics.CodeAnalysis.MaybeNull]
        object Deserialize(string content, Type type, string contentType);
    }
}
