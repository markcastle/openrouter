using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides serialization and deserialization services for OpenRouter models.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the specified value to a string (e.g., JSON).
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The serialized string representation.</returns>
        string Serialize<T>(T value);

        /// <summary>
        /// Deserializes the specified string to a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The serialized string (e.g., JSON).</param>
        /// <returns>The deserialized value.</returns>
        T Deserialize<T>(string json);

        /// <summary>
        /// Deserializes the specified string to an object of the given type.
        /// </summary>
        /// <param name="json">The serialized string (e.g., JSON).</param>
        /// <param name="type">The runtime type to deserialize to.</param>
        /// <returns>The deserialized object.</returns>
        object Deserialize(string json, Type type);
    }
}
