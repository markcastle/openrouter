using System;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.DependencyInjection
{
    /// <summary>
    /// Factory for creating the appropriate ISerializer implementation based on configuration.
    /// </summary>
    public static class SerializerFactory
    {
        /// <summary>
        /// Creates an ISerializer based on the specified serializer type.
        /// </summary>
        /// <param name="serializerType">The serializer type (e.g., "SystemTextJson", "NewtonsoftJson").</param>
        /// <returns>An implementation of ISerializer.</returns>
        /// <exception cref="ArgumentException">Thrown if the serializer type is unknown.</exception>
        public static ISerializer Create(string serializerType)
        {
            switch (serializerType?.Trim().ToLowerInvariant())
            {
                case "systemtextjson":
                    return new OpenRouter.Client.SystemTextJson.SystemTextJsonSerializer();
                case "newtonsoftjson":
                    return new OpenRouter.Client.NewtonsoftJson.NewtonsoftJsonSerializer();
                default:
                    throw new ArgumentException($"Unknown serializer type: {serializerType}", nameof(serializerType));
            }
        }
    }
}
