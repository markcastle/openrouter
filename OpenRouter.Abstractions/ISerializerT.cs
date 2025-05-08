namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Strongly-typed serializer for advanced scenarios.
    /// </summary>
    /// <typeparam name="T">The type to serialize/deserialize.</typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// Serializes the specified value to a string (e.g., JSON).
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The serialized string representation.</returns>
        string Serialize(T value);

        /// <summary>
        /// Deserializes the specified string to a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="json">The serialized string (e.g., JSON).</param>
        /// <returns>The deserialized value.</returns>
        T Deserialize(string json);
    }
}
