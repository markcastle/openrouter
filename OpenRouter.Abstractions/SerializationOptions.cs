namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Options for serialization configuration.
    /// </summary>
    public class SerializationOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the serializer type ("SystemTextJson", "NewtonsoftJson").
        /// </summary>
        public string? SerializerType { get; set; } = "SystemTextJson";

        /// <summary>
        /// Gets or sets a value indicating whether to ignore null values during serialization.
        /// </summary>
        public bool IgnoreNullValues { get; set; } = true;

        /// <inheritdoc/>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SerializerType))
            {
                throw new OpenRouterSerializationException("SerializerType must be specified.");
            }
        }
    }
}
