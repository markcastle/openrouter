using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenRouter.Client.NewtonsoftJson
{
    /// <summary>
    /// Example custom converter for DateTimeOffset, demonstrating how to implement converters for complex types.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        /// <summary>
        /// Writes the DateTimeOffset value as an ISO 8601 string.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="serializer">The serializer instance.</param>
        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("o"));
        }

        /// <summary>
        /// Reads and converts the JSON to a DateTimeOffset.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="hasExistingValue">Whether there is an existing value.</param>
        /// <param name="serializer">The serializer instance.</param>
        /// <returns>The parsed DateTimeOffset value.</returns>
        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = reader.Value?.ToString();
            return str != null ? DateTimeOffset.Parse(str) : default;
        }
    }
}
