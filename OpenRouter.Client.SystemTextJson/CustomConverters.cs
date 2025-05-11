using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Example custom converter for DateTimeOffset to ensure ISO 8601 format.
    /// </summary>
    /// <summary>
    /// Example custom converter for DateTimeOffset to ensure ISO 8601 format.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        /// <summary>
        /// Reads and converts the JSON to a DateTimeOffset.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>The parsed DateTimeOffset value.</returns>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString()!);
        }

        /// <summary>
        /// Writes the DateTimeOffset value as an ISO 8601 string.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="options">Serializer options.</param>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("o"));
        }
    }

    // Add additional converters as needed for complex types.
}
